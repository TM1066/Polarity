using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "Map")]
public class Map : ScriptableObject
{
    public AudioClip song;
    [SerializeField] float songBPM; //the BPM of the song
    private float songCrotchetLength; //how long a crotchet/beat is - in seconds
    public Image thumbnail;
    [Header ("Should Both Be the Same Length")]
    public List<float> noteBeats = new List<float>(); // might use dictionary
    public List<int> notePositions = new List<int>();


    public void AddNoteAtBeat(float beat, int lanePosition)
    {
        noteBeats.Add(beat);
        notePositions.Add(lanePosition);
    }

    public float GetBeatLength() // in Seconds
    {
        return 60f / songBPM;
    }
}
