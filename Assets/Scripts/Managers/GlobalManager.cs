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

    public static Color cutePink = new Color(1, 0.6078432f, 0.909804f,1);
    public static Color cuteBlue = new Color(0.6273585f,0.9888251f,1,1);

    public static string mapFileDirectory = Path.Combine(Application.persistentDataPath, "gameMaps.json");

    public static bool recordingMode = false;

    //options stuff
    public static float gameVolume = 1f;


    // LEADERBOARD LOADING NONSENSE
    //private static string leaderboardFilePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");
    private static string leaderboardFilePath; // this is defined by the gameTracker on start up, this is very dumb and i hate it, blame Unity
    public static List<User> leaderboard = new List<User>();
    public static bool[] leaderBoardChanged = new bool[10] {false,false,false,false,false,false,false,false,false,false}; // Remember to change all back to false upon replay in end scene

    public static void LoadLeaderboard()
    {
        if (File.Exists(leaderboardFilePath))
        {
            string json = File.ReadAllText(leaderboardFilePath);

            LeaderboardWrapper wrapper = JsonUtility.FromJson<LeaderboardWrapper>(json);
            leaderboard = wrapper.users;
            Debug.Log("Leaderboard loaded!");
        }
        else
        {
            Debug.Log("No leaderboard file found; starting fresh.");
            SaveLeaderboard();
        }
    }

    public static void SaveLeaderboard()
    {
        string json = JsonUtility.ToJson(new LeaderboardWrapper(leaderboard), true);
        File.WriteAllText(leaderboardFilePath, json);
        Debug.Log("Leaderboard saved to: " + leaderboardFilePath);
    }
}
