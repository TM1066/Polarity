using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Map")]
public class Map : ScriptableObject
{
    public AudioClip song;
    public List<float> noteTimings = new List<float>();

    public GameObject inputArea;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
