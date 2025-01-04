using UnityEngine;
using UnityEngine.UI;

public class NoteLane : MonoBehaviour
{
    //will define where the lane borders are drawn
    public float width;
    //will define the top and bottom of the lane, should be off-screen
    public float noteSpawnPosition; 
    public float noteDespawnPosition; 

    public LineRenderer lineRenderer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Should look kinda like a bucket
        lineRenderer.SetPosition(0, new Vector3(-width, noteSpawnPosition, 0));
        lineRenderer.SetPosition(1, new Vector3(-width, noteDespawnPosition, 0));
        lineRenderer.SetPosition(2, new Vector3(width, noteDespawnPosition, 0));
        lineRenderer.SetPosition(3, new Vector3(width, noteSpawnPosition, 0));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
