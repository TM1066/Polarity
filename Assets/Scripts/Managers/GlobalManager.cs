using System.IO;
using UnityEngine;
using System.Collections.Generic;

public static class GlobalManager
{
    //Hard to do at the moment
    public static KeyCode Key1 = KeyCode.A;
    public static KeyCode Key2 = KeyCode.S;
    public static KeyCode Key3 = KeyCode.D;
    public static KeyCode Key4 = KeyCode.F;
    public static KeyCode Key5 = KeyCode.G;

    public static Map currentSelectedMap;
    public static int currentScore = 0;
    public static int currentCombo = 0;

    public static int perfectScoreIncrement = 5;
    public static int greatScoreIncrement = 3;
    public static int okScoreIncrement = 1;
    public static int badScoreIncrement = 1;

    public static bool lolzMode = false; //Stops particle effects

    public static Color cutePink = new Color(1, 0.6078432f, 0.909804f, 1);
    public static Color cuteBlue = new Color(0.6273585f,0.9888251f,1,1);

    //public static string mapFileDirectory = Path.Combine(Application.persistentDataPath, "gameMaps.json");

    public static bool recordingMode = true;

    //options stuff
    public static float gameVolume = 1f;


    // map.mapLeaderboard LOADING NONSENSE
    //private static string map.mapLeaderboardFilePath = Path.Combine(Application.persistentDataPath, "map.mapLeaderboard.json");
    private static string mapLeaderboardFilePath; // this is defined by the gameTracker on start up, this is very dumb and i hate it, blame Unity
    
    public static void SetMapLeaderboardFilePath(string path)
    {
        mapLeaderboardFilePath = path;
    }

    // public static void LoadMapLeaderboard(Map map)
    // {
    //     if (File.Exists(mapLeaderboardFilePath))
    //     {
    //         string json = File.ReadAllText(mapLeaderboardFilePath);

    //         LeaderboardWrapper wrapper = JsonUtility.FromJson<LeaderboardWrapper>(json);
    //         map.mapLeaderboard = wrapper.users;
    //         Debug.Log("mapLeaderboard loaded!");
    //     }
    //     else
    //     {
    //         Debug.Log("No map.mapLeaderboard file found; starting fresh.");
    //         SaveMapLeaderboard(map);
    //     }
    // }

    // public static void SaveMapLeaderboard(Map map)
    // {
    //     SetMapLeaderboardFilePath(Path.Combine(Application.persistentDataPath, "map.mapLeaderboardof" + map.name + ".json"));
    //     string json = JsonUtility.ToJson(new LeaderboardWrapper(map.mapLeaderboard), true);
    //     File.WriteAllText(mapLeaderboardFilePath, json);
    //     Debug.Log("map.mapLeaderboard saved to: " + mapLeaderboardFilePath);
    // }

    // public static void AddUserToMapLeaderboard(User user, Map map)
    // {
    //     // check if the Player's score is higher than the lowest score on map.mapLeaderboard / the lowest entry is empty

    //     if (map.mapLeaderboard.Count == 10 && currentScore > map.mapLeaderboard[9].score)
    //     {
    //         map.mapLeaderboard.RemoveAt(9);
    //         map.mapLeaderboard.Add(user);
    //     }
    //     else if (map.mapLeaderboard.Count < 10)
    //     {
    //         map.mapLeaderboard.Add(user);
    //     }

    //     // Sort map.mapLeaderboard by score (descending)
    //     map.mapLeaderboard.Sort((a, b) => b.score.CompareTo(a.score));

    //     if (map.mapLeaderboard.IndexOf(user) >= 0) // will return -1 if the user hasn't been added
    //     {
    //         map.GetMapLeaderboardChanged()[map.mapLeaderboard.IndexOf(user)] = true;
    //     }

    //     SaveMapLeaderboard(map);
    // }
}
