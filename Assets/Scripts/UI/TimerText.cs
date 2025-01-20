using UnityEngine;
using TMPro;
using System;

public class TimerText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public SongManager songManager;

    void Start()
    {
        string formattedTime = ScriptUtils.GetPrettyFormattedTime(songManager.currentMap.song.length / songManager.audioPlayer.pitch);
        string formattedCurrentTime = ScriptUtils.GetPrettyFormattedTime(songManager.songPosition);

        text.text = $"Time: {formattedCurrentTime} / {formattedTime}";
    }

    // Update is called once per frame
    void Update()
    {
        if (songManager.audioPlayer.isPlaying)
        {
            string formattedTime = ScriptUtils.GetPrettyFormattedTime(songManager.currentMap.song.length / songManager.audioPlayer.pitch);
            string formattedCurrentTime = ScriptUtils.GetPrettyFormattedTime(songManager.songPosition);

            text.text = $"Time: {formattedCurrentTime} / {formattedTime}";
        }
    }
}
