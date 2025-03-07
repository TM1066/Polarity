using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class NoteLane : MonoBehaviour
{
    //will define where the lane borders are drawn
    public float width;
    //will define the top and bottom of the lane, should be off-screen
    public Transform noteSpawnPosition; 
    public float noteDespawnPosition; 
    public InputArea inputArea;

    public float fadeAmount = 0.38f; // should be between around 0.01 and 0.30~

    public float noteSpeed = 2;

    public GameObject notePrefab;

    public LineRenderer lineRenderer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float leftLocation = this.transform.position.x - width;
        float rightLocation = this.transform.position.x + width;
        float topLocation = this.transform.position.y + noteSpawnPosition.position.y;  
        float bottomLocation = this.transform.position.y + noteDespawnPosition; 

        //Should look kinda like a bucket
        lineRenderer.SetPosition(0, new Vector3(leftLocation, topLocation, this.transform.position.z));
        lineRenderer.SetPosition(1, new Vector3(leftLocation, bottomLocation, this.transform.position.z));
        lineRenderer.SetPosition(2, new Vector3(rightLocation, bottomLocation, this.transform.position.z));
        lineRenderer.SetPosition(3, new Vector3(rightLocation, topLocation, this.transform.position.z));

        var randomGradient = new Gradient();

        randomGradient.colorKeys = new GradientColorKey[]
        {
            new GradientColorKey(Color.clear, 0 + fadeAmount),
            new GradientColorKey(ScriptUtils.GetRandomColorFromString("Bungoscrunglingli"), 0.51f),
            new GradientColorKey(ScriptUtils.GetComplimentaryColor(ScriptUtils.GetRandomColorFromString("TM")), 0.49f),
            new GradientColorKey(Color.clear, 1 - fadeAmount)
        };
        lineRenderer.colorGradient = randomGradient;

        //StartCoroutine(SpawnNotesDebug());
    }

    // Update is called once per frame
    void Update()
    {
        //Testing it changing
        // float leftLocation = this.transform.position.x - width;
        // float rightLocation = this.transform.position.x + width;
        // float topLocation = this.transform.position.y + noteSpawnPosition.position.y;  
        // float bottomLocation = this.transform.position.y + noteDespawnPosition; 

        // lineRenderer.SetPosition(0, new Vector3(leftLocation, topLocation, this.transform.position.z));
        // lineRenderer.SetPosition(1, new Vector3(leftLocation, bottomLocation, this.transform.position.z));
        // lineRenderer.SetPosition(2, new Vector3(rightLocation, bottomLocation, this.transform.position.z));
        // lineRenderer.SetPosition(3, new Vector3(rightLocation, topLocation, this.transform.position.z));

        // var randomGradient = new Gradient();

        // randomGradient.colorKeys = new GradientColorKey[]
        // {
        //     new GradientColorKey(Color.clear, 0 + fadeAmount),
        //     new GradientColorKey(GlobalManager.cutePink, 0.51f),
        //     new GradientColorKey(GlobalManager.cuteBlue, 0.49f),
        //     new GradientColorKey(Color.clear, 1 - fadeAmount)
        // };
        // lineRenderer.colorGradient = randomGradient;
    }

    public void SpawnNote()
    {
        GameObject note = Instantiate(notePrefab, noteSpawnPosition);
        Note noteComponent = note.GetComponent<Note>();
        noteComponent.despawnHeight = noteDespawnPosition;

        GameObject.Find("SongManager").GetComponent<SongManager>().spawnedNotes.Add(note);

        var noteSpriteRenderer = note.GetComponent<SpriteRenderer>();
        noteSpriteRenderer.color = Color.clear;

        //fade in from clear
        StartCoroutine(ScriptUtils.ColorLerpOverTime(noteSpriteRenderer, noteSpriteRenderer.color, Color.white, 0.5f / noteSpeed)); 
       //StartCoroutine(ScriptUtils.PositionLerp(note.transform, note.transform.position, new Vector2 (this.transform.position.x, inputArea.transform.position.y), noteSpeed));

        StartCoroutine(MoveNotesProperly(note));
    }

    IEnumerator MoveNotesProperly(GameObject note){
        //Debug.Log("noteSpeed = "+noteSpeed);

        Vector2 startPos = note.transform.position;
        Vector2 targetPos = new Vector2(this.transform.position.x, inputArea.transform.position.y);

        float timeElapsed = 0;
        //moving it to the noteInputArea in time with the song
        while (timeElapsed < noteSpeed && note) 
        {
            note.transform.position = Vector2.Lerp(startPos, targetPos, timeElapsed / noteSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        if (note)
        {
            note.transform.position = targetPos;
            startPos = note.transform.position;
            targetPos = new Vector2(this.transform.position.x, inputArea.transform.position.y - Mathf.Abs(inputArea.transform.position.y - noteSpawnPosition.position.y));
        }
        
        //Keep it moving and delete
        timeElapsed = 0;

        while (timeElapsed < noteSpeed && note) 
        {
            note.transform.position = Vector3.Slerp(startPos, targetPos, timeElapsed / noteSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        if (note)
        {
            Destroy(note);
        }
    }

    public IEnumerator SpawnNotesDebug()
    {
        while (true)
        {
            try 
            {
                //Debug.Log("Note Should Spawn");
                System.Random random = new();
                if (random.Next(3) == 1)
                {
                    GameObject note = Instantiate(notePrefab, noteSpawnPosition);
                    note.transform.position = new Vector3(note.transform.position.x, note.transform.position.y, noteSpawnPosition.position.z);
                    Note noteComponent = note.GetComponent<Note>();
                    noteComponent.despawnHeight = noteDespawnPosition + 1;

                    GameObject.Find("SongManager").GetComponent<SongManager>().spawnedNotes.Add(note);

                    var noteSpriteRenderer = note.GetComponent<SpriteRenderer>();
                    noteSpriteRenderer.color = Color.clear;

                    StartCoroutine(ScriptUtils.ColorLerpOverTime(noteSpriteRenderer, noteSpriteRenderer.color, Color.white,0.5f));
                    StartCoroutine(ScriptUtils.PositionLerp(note.transform, note.transform.position, new Vector2 (this.transform.position.x, noteDespawnPosition), noteSpeed));
                }
            }
            catch (Exception e)
            {
                Debug.Log("Error in spawning notes: " + e.Message);
            }
            yield return new WaitForSeconds(0.7f);
        }
    }
}
