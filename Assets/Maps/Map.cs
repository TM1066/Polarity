using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

[CreateAssetMenu(fileName = "Map")]
public class Map : ScriptableObject
{
    public AudioClip song;
    [Header ("Should Both Be the Same Length")]
    public List<float> noteTimings = new List<float>(); // might use dictionary
    public List<int> notePositions = new List<int>();

    public Image thumbnail;

    public void AddNoteAtTime(float time, int lanePosition)
    {
        noteTimings.Add(time);
        notePositions.Add(lanePosition);
    }
}
