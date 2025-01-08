using UnityEngine;
using TMPro;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class SongManager : MonoBehaviour
{
    public AudioClip currentSong;
    public AudioSource audioPlayer;
    public Map currentMap;

    public TextMeshProUGUI titleText;

    public List<GameObject> spawnedNotes = new List<GameObject>();

    public float noteSpawnOffset; // how long it will take the note to reach the input area after being spawned

    public float songPosition;

    public NoteLane[] noteLanes = new NoteLane[5];
    public List<bool> notesSpawned;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSong = currentMap.song;
        audioPlayer.clip = currentSong;
        titleText.text = currentSong.name;

        audioPlayer.Play();

        notesSpawned = Enumerable.Repeat(false, currentMap.noteBeats.Count).ToList();

        //not the best fix, but noteLands[0] should return the same as every other noteLane if it's going well
        noteSpawnOffset = (noteLanes[0].noteSpawnPosition.position.y - noteLanes[0].inputArea.transform.position.y) / noteLanes[0].noteSpeed;
        Debug.Log("note spawn offset: " + noteSpawnOffset);

    }
    // Update is called once per frame
    void Update()
    {
        if ((float)((audioPlayer.time) * audioPlayer.pitch) - currentMap.startUpCountdown >= 0)
        {
            songPosition = (float)((audioPlayer.time) * audioPlayer.pitch) - currentMap.startUpCountdown;
        }
        
        // Debug.Log("Current Song Position: " + songPosition);
        // Debug.Log("Current Beat: " + GetCurrentBeat());
        // Debug.Log("Current Closest Full Beat: " + GetCurrentClosestBeat());
        // Debug.Log("Current Closest Half Beat: " + GetCurrentClosestHalfBeat());

        int index = 0;

        if (!GlobalManager.recordingMode)
        {
            foreach (float timing in currentMap.noteBeats) // this is the enumerate thing from arcane aristocracy!
            {
                float targetBeat = timing - noteSpawnOffset;
                float currentBeat = GetCurrentClosestHalfBeat();
                Debug.Log($"Timing: {timing}, Target Beat: {targetBeat}, Current Beat: {currentBeat}");


                if (!notesSpawned[index] && Mathf.Abs((timing - noteSpawnOffset) - GetCurrentClosestHalfBeat()) <= 0.03f) // might return true more than once, may need to check that
                {
                    Debug.Log($"Spawning note at index {index}, Target Beat: {targetBeat}, Current Beat: {currentBeat}");
                    noteLanes[currentMap.notePositions[index] - 1].SpawnNote();
                    notesSpawned[index] = true;
                }
                index++;
            }
        }
    }

    public int GetCurrentClosestBeat()
    {
        return Mathf.RoundToInt((songPosition / currentMap.GetBeatLength()));
    }

    public float GetCurrentClosestHalfBeat()
    {
        return (float) Math.Round((GetCurrentBeat() * 2),MidpointRounding.AwayFromZero) / 2;
    }

    public float GetCurrentBeat()
    {        
        return songPosition / currentMap.GetBeatLength();
    }
}
