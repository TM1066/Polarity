using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Microsoft.Unity.VisualStudio.Editor;

public class SongManager : MonoBehaviour
{
    [Header ("Map and Song")]
    public AudioClip currentSong;
    public AudioSource audioPlayer;
    public Map currentMap;

    public SpriteRenderer dimmingSquare;
    public TextMeshProUGUI countdownTextMesh;
    private int countDownLeft = 0;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI songOverText;
    public GameObject leaderboardGameObject;
    public UnityEngine.UI.Image leaderboardBGImage;
    public List<TextMeshProUGUI> leaderboardTextsToDim = new List<TextMeshProUGUI>();

    public List<GameObject> spawnedNotes = new List<GameObject>();

    public float targetPitch = 1f;
    public float targetVolume = 1f;
    public float noteSpawnOffset; // how long it will take the note to reach the input area after being spawned

    public float songPosition;

    public NoteLane[] noteLanes = new NoteLane[5];
    public List<bool> notesSpawned;

    public bool recordingMode;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        GlobalManager.recordingMode = recordingMode;
        currentMap = GlobalManager.currentSelectedMap;

        currentSong = currentMap.song;
        audioPlayer.clip = currentSong;
        titleText.text = currentSong.name;
        leaderboardBGImage.color = Color.clear;
        foreach (TextMeshProUGUI textMesh in leaderboardTextsToDim)
        {
            textMesh.color = Color.clear;
        }

        notesSpawned = Enumerable.Repeat(false, currentMap.noteBeats.Count).ToList();

        //not the best fix, but noteLands[0] should return the same as every other noteLane if it's going well
        noteSpawnOffset = (noteLanes[0].noteSpawnPosition.position.y - noteLanes[0].inputArea.transform.position.y) / noteLanes[0].noteSpeed;
        Debug.Log("note spawn offset: " + noteSpawnOffset);

        foreach (NoteLane noteLane in noteLanes)
        {
            noteLane.noteSpeed /= audioPlayer.pitch;
        }

        songPosition = 0;
        //starting Coroutines
        countDownLeft = currentMap.startUpCountdown;
        StartCoroutine(Countdown());
        StartCoroutine(SongOverCheckAndHandling());
    }
    // Update is called once per frame
    void Update(){
        if (countDownLeft <= 0)
        {
            songPosition = (float)(audioPlayer.time / audioPlayer.pitch);
        }
        
        //  Debug.Log("Current Song Position: " + songPosition);
        //  Debug.Log("Current Beat: " + GetCurrentBeat());
        //  Debug.Log("Current Closest Full Beat: " + GetCurrentClosestBeat());
        //  Debug.Log("Current Closest Half Beat: " + GetCurrentClosestHalfBeat());

        //spawning notes
        int index = 0;
    
        if (!GlobalManager.recordingMode)
        {
            foreach (float timing in currentMap.noteBeats) // this is the enumerate thing from arcane aristocracy!
            {
                float targetBeat = timing - noteSpawnOffset;
                float currentBeat = GetCurrentClosestHalfBeat();
                //Debug.Log($"Timing: {timing}, Target Beat: {targetBeat}, Current Beat: {currentBeat}");

                // A little hard to read but is basically just less sensitive to Floating point inaccuracy but is a lil inaccurate
                if (!notesSpawned[index] && Mathf.Abs((timing - noteSpawnOffset) - GetCurrentClosestHalfBeat()) <= 0.03f) 
                {
                    Debug.Log($"Spawning note at index {index}, Target Beat: {targetBeat}, Current Beat: {currentBeat}");
                    noteLanes[currentMap.notePositions[index] - 1].SpawnNote();
                    notesSpawned[index] = true;
                }
                index++;
            }
        }
    }

    IEnumerator SongOverCheckAndHandling(){
        while (true){
            //Debug.Log($"Current Song Position: {songPosition}\n current song Length: {currentSong.length}");
            if (songPosition >= currentSong.length){
                StartCoroutine(ScriptUtils.ColorLerpOverTime(dimmingSquare, Color.clear, ScriptUtils.GetColorButDifferentAlpha(Color.black, -0.5f), 1f));
                yield return new WaitForSeconds(1f);
                audioPlayer.volume = 0f;
                audioPlayer.Stop();

                if (GlobalManager.currentScore == currentMap.noteBeats.Count * GlobalManager.perfectScoreIncrement){
                    songOverText.text = "Perfect Combo!!";
                    //setaudiosource to yay audio
                }
                else if(GlobalManager.currentCombo >= currentMap.noteBeats.Count){
                    songOverText.text = "Full Combo!!";
                    //setaudiosource to yay audio
                }
                else {
                    songOverText.text = $"Final Score:  {GlobalManager.currentScore}/{currentMap.noteBeats.Count * GlobalManager.perfectScoreIncrement}";
                    //setaudiosource to ok audio
                }

                StartCoroutine(ScriptUtils.ColorLerpOverTime(songOverText, Color.clear, Color.white, 0.5f));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ScriptUtils.ColorLerpOverTime(songOverText, Color.white, Color.clear, 0.5f));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ScriptUtils.ColorLerpOverTime(songOverText, Color.clear, Color.white, 0.5f));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ScriptUtils.ColorLerpOverTime(songOverText, Color.white, Color.clear, 0.5f));
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(ScriptUtils.ColorLerpOverTime(songOverText, Color.clear, Color.white, 0.5f));
                yield return new WaitForSeconds(0.5f);
                

                //Dim everything else in scene and bring leaderboard visual
                StartCoroutine(ScriptUtils.ColorLerpOverTime(dimmingSquare, dimmingSquare.color, Color.clear, 1f));
                StartCoroutine(ScriptUtils.ColorLerpOverTime(songOverText, songOverText.color, Color.clear, 1f));
                yield return new WaitForSeconds(1f);

                //bring up leaderboard
                StartCoroutine(ScriptUtils.ValueLerpOverFixedTime(
                newValue => audioPlayer.volume = newValue,    // Setter
                () => audioPlayer.volume,                    // Getter
                1.0f,                                        // Final value
                2.0f                                         // Duration
                ));
                audioPlayer.Play();
            }

            leaderboardGameObject.SetActive(true);
            ShowLeaderBoard();

            yield return null;
        }
    }

    IEnumerator Countdown() // countdown to the beat, count in
    {
        dimmingSquare.color = Color.clear;
        countdownTextMesh.color = Color.clear;

        if (countDownLeft > 0)
        {
            Time.timeScale = 0;
            dimmingSquare.color = ScriptUtils.GetColorButDifferentAlpha(dimmingSquare.color, 0.7f);
        }
        else 
        {
            dimmingSquare.color = Color.clear;
        }

        while (countDownLeft > 0)
        {
            countdownTextMesh.text = countDownLeft.ToString();

            float timeElapsed = 0;
            while (timeElapsed < currentMap.GetBeatLength() * 10)
            {
                countdownTextMesh.color = Color.Lerp(countdownTextMesh.color, Color.white, timeElapsed / currentMap.GetBeatLength());
                float textScale = Mathf.SmoothStep(countdownTextMesh.transform.localScale.x, 144, timeElapsed / currentMap.GetBeatLength());

                countdownTextMesh.fontSize = textScale;

                timeElapsed += Time.fixedDeltaTime;
                yield return null;
            }

            // Ensure the final color is set, in case the loop doesn't hit it exactly.
            countdownTextMesh.color = Color.white;

            timeElapsed = 0;
            while (timeElapsed < currentMap.GetBeatLength() * 10)
            {
                countdownTextMesh.color = Color.Lerp(countdownTextMesh.color, Color.clear, timeElapsed / currentMap.GetBeatLength());
                float textScale = Mathf.SmoothStep(countdownTextMesh.transform.localScale.x, 72, timeElapsed / currentMap.GetBeatLength());

                countdownTextMesh.fontSize = textScale;
                
                timeElapsed += Time.fixedDeltaTime;
                yield return null;
            }
            // Ensure the final color is set, in case the loop doesn't hit it exactly.
            countdownTextMesh.color = Color.clear;

            countDownLeft -= 1;
        }
        
        audioPlayer.Play();
        audioPlayer.volume = 0;
        Time.timeScale = 1;

        // Ensure the final color is set, in case the loop doesn't hit it exactly.
        countdownTextMesh.color = Color.clear;

        StartCoroutine(ScriptUtils.ColorLerpOverTime(dimmingSquare, dimmingSquare.color, Color.clear, currentMap.GetBeatLength() * 10));
        StartCoroutine(ScriptUtils.ValueLerpOverFixedTime(
        newValue => audioPlayer.volume = newValue,    // Setter
        () => audioPlayer.volume,                    // Getter
        1.0f,                                        // Final value
        3.0f                                         // Duration
        ));

        countDownLeft = 0;
        yield return null;
    }

    void HideLeaderBoard(){
        leaderboardBGImage.color = Color.clear;
        foreach (TextMeshProUGUI textMesh in leaderboardTextsToDim)
        {
            textMesh.color = Color.clear;
        }
    }
    void ShowLeaderBoard(){
        leaderboardBGImage.color = Color.white;
        foreach (TextMeshProUGUI textMesh in leaderboardTextsToDim)
        {
            textMesh.color = Color.black;
        }
    }

    public int GetCurrentClosestBeat(){
        return Mathf.RoundToInt(GetCurrentBeat());
    }

    public float GetCurrentClosestHalfBeat(){
        return (float) Math.Round((GetCurrentBeat() * 2),MidpointRounding.AwayFromZero) / 2;
    }

    public float GetCurrentBeat(){        
        return songPosition / (currentMap.GetBeatLength() / audioPlayer.pitch);
    }
}
