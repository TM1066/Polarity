using UnityEngine;
using TMPro;
using System;

public class TimerText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public SongManager songManager;

    // Update is called once per frame
    void Update()
    {
        if (songManager.audioPlayer.isPlaying)
        {
            TimeSpan time = TimeSpan.FromSeconds(Convert.ToInt32(songManager.currentSong.length));
            string formattedTime = string.Format("{0}:{1:00}", (int)time.TotalMinutes, time.Seconds);

            TimeSpan currentTime = TimeSpan.FromSeconds(Convert.ToInt32(songManager.songPosition));
            string formattedCurrentTime = string.Format("{0}:{1:00}", (int)currentTime.TotalMinutes, currentTime.Seconds);

            text.text = $"Time: {formattedCurrentTime} / {formattedTime}";
        }

    }
}
