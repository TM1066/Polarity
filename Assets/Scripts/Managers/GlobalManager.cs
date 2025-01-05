using System.IO;
using UnityEngine;

public static class GlobalManager
{
    //Hard to do at the moment
    public static KeyCode Key1 = KeyCode.A;
    public static KeyCode Key2 = KeyCode.S;
    public static KeyCode Key3 = KeyCode.D;
    public static KeyCode Key4 = KeyCode.F;
    public static KeyCode Key5 = KeyCode.G;

    public static int currentScore = 0;

    public static Color cutePink = new Color(1, 0.6078432f, 0.909804f,1);
    public static Color cuteBlue = new Color(0.6273585f,0.9888251f,1,1);

    public static string mapFileDirectory = Path.Combine(Application.persistentDataPath, "gameMaps.json");

    public static bool recordingMode = false;
}
