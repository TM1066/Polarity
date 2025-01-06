using UnityEngine;
using TMPro;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSong = currentMap.song;
        audioPlayer.clip = currentSong;
        titleText.text = currentSong.name;

        audioPlayer.Play();

        Debug.Log("Song Start Time: " + AudioSettings.dspTime);
    }

    // Update is called once per frame
    void Update()
    {
        songPosition = (float)(audioPlayer.time) * audioPlayer.pitch;
        Debug.Log("Current Song Position: " + songPosition);
        Debug.Log("Current Beat: " + GetCurrentBeat());

        int index = 0;

        if (!GlobalManager.recordingMode)
        {
            foreach (float timing in currentMap.noteBeats) // this is the enumerate thing from arcane aristocracy!
            {
                if (Mathf.Approximately(timing, audioPlayer.time))
                {
                    noteLanes[currentMap.notePositions[index - 1] - 1].SpawnNote();
                }
                index++;
            }
        }
    }

    public int GetCurrentClosestBeat()
    {
        return (int) (songPosition / currentMap.GetBeatLength());
    }

    public float GetCurrentBeat()
    {
        return songPosition / currentMap.GetBeatLength();
    }
}
