using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MapDisplayObject : MonoBehaviour
{
    public Map mapToDisplay;

    public Image thumbnailImage;
    public Image bgImage;

    public TextMeshProUGUI songNameTextMesh;
    public TextMeshProUGUI songLengthTextMesh;
    public TextMeshProUGUI composerNameTextMesh;
    public TextMeshProUGUI mapperNameTextMesh;

    private float startY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startY = this.transform.position.y;

        thumbnailImage.sprite = mapToDisplay.thumbnail;

        songNameTextMesh.text = mapToDisplay.song.name;
        songLengthTextMesh.text = ScriptUtils.GetPrettyFormattedTime(mapToDisplay.GetSongLengthInSeconds() + mapToDisplay.startUpCountdown);
        composerNameTextMesh.text = "Composer\n" + mapToDisplay.composer;
        mapperNameTextMesh.text = "MapMaker\n" + mapToDisplay.mapper;
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
