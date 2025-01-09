using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.iOS.Xcode;
using UnityEngine;
//#if UNITY_EDITOR
using UnityEditor;
//#endif

public enum MapDifficulties{
    VeryEasy,
    Easy,
    Moderate,
    Hard,
    VeryHard
}

[CreateAssetMenu(fileName = "Map")]
public class Map : ScriptableObject
{
    public AudioClip song;
    public float songBPM; //the BPM of the song
    private float songCrotchetLength; //how long a crotchet/beat is - in seconds
    public Sprite thumbnail;
    [Header ("Should Both Be the Same Length")]
    public List<float> noteBeats = new List<float>(); // might use dictionary
    public List<int> notePositions = new List<int>();

    public float startUpCountdown; // in Seconds

    [Header ("Metadata")]
    public string composer;
    public string mapper;
    public MapDifficulties mapDifficulty;

    public void AddNoteAtBeat(float beat, int lanePosition)
    {
        noteBeats.Add(beat);
        notePositions.Add(lanePosition);
    }

    public float GetBeatLength() // in Seconds
    {
        return 60f / songBPM;
    }

    public float GetSongLengthInSeconds()
    {
        return song.length - startUpCountdown;
    }

    public string GetSongName()
    {
        return song.name;
    }

    public void SaveChanges()
    {
        #if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        #endif
    }
}
