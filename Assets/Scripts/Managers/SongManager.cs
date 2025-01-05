using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class SongManager : MonoBehaviour
{
    public AudioClip currentSong;

    public TextMeshProUGUI titleText;

    public List<GameObject> spawnedNotes = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        titleText.text = currentSong.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
