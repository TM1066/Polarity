using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MapDisplayObject : MonoBehaviour
{
    public Map mapToDisplay;

    public SpriteRenderer thumbnailSpriteRenderer;

    public TextMeshProUGUI songNameTextMesh;
    public TextMeshProUGUI songLengthTextMesh;
    public TextMeshProUGUI composerNameTextMesh;
    public TextMeshProUGUI mapperNameTextMesh;

    private float startY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startY = this.transform.position.y;

        thumbnailSpriteRenderer.sprite = mapToDisplay.thumbnail;

        songNameTextMesh.text = mapToDisplay.song.name;
        songLengthTextMesh.text = ScriptUtils.GetPrettyFormattedTime(mapToDisplay.GetSongLengthInSeconds());
        composerNameTextMesh.text = "Composer\n" + mapToDisplay.composer;
        mapperNameTextMesh.text = "MapMaker\n" + mapToDisplay.mapper;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayMap()
    {
        GlobalManager.currentSelectedMap = mapToDisplay;
        SceneManager.LoadScene("MapPlayer");
    }

    float GetAlphaFromY(float colorAlpha, float y)
    {
        // not sure about this until I can test it
        return colorAlpha - Mathf.Abs(startY - y);
    }

}
