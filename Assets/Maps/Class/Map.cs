using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
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
    public List<float> noteBeats = new List<float>(); // might use dictionary - doesn't serialise nice with unity GUI right now though
    public List<int> notePositions = new List<int>();
    [Range(3,10)]
    public int startUpCountdown = 3; // in Seconds

    [Header ("Metadata")]
    public string composer;
    public string mapper;
    public MapDifficulties mapDifficulty;
    // public List<User> mapLeaderboard = new List<User>() {null, null, null, null, null, null, null, null, null, null};
    // private List<bool> mapLeaderboardChanged = new List<bool> () {false,false,false,false,false,false,false,false,false,false};

    [SerializeField]
    private int mapHighScore;

    public int MapHighScore
    {
        get => mapHighScore;
        set
        {
            if (mapHighScore == value)
                return;

            mapHighScore = value;

    #if UNITY_EDITOR
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
    #endif
        }
    }

    // public List<bool> GetMapLeaderboardChanged()
    // {
    //     return mapLeaderboardChanged;
    // }

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

    //Should eventually be replaced by equation to calculate relative difficulty using amount of notes modified by BPM
    public MapDifficulties GetSongDifficulty()
    {
        return mapDifficulty;
    }

    public void SaveChanges()
    {
        #if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();
        #endif
    }
}
