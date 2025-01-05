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
        text.text = $"Time: {Convert.ToInt32(songManager.audioPlayer.time)} / {Convert.ToInt32(songManager.currentSong.length)}";
    }
}
