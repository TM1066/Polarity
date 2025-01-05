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

    //public float songTimer = 0f;

    public NoteLane[] noteLanes = new NoteLane[5];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSong = currentMap.song;
        audioPlayer.clip = currentSong;
        titleText.text = currentSong.name;

        audioPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        int index = 0;

        if (!GlobalManager.recordingMode)
        {
            foreach (float timing in currentMap.noteTimings) // this is the enumerate thing from arcane aristocracy!
            {
                if (Mathf.Approximately(timing, audioPlayer.time))
                {
                    noteLanes[currentMap.notePositions[index - 1] - 1].SpawnNote();
                }
                index++;
            }
        }
    }
}
