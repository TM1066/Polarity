using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using System;

public class InputArea : MonoBehaviour
{
    public NoteLane noteLane;
    public SongManager songManager;

    public KeyCode key;

    [Header ("Visual Stuff")]
    public SpriteRenderer spriteRenderer;
    public Sprite neutralSprite;
    public Sprite pressedSprite;

    public ParticleSystem particleSys;

    public TextMeshProUGUI keyCodeDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer.sprite = neutralSprite;

        noteLane = GetComponentInParent<NoteLane>();

        keyCodeDisplay.text = $"{key}";
    }

    // Update is called once per frame
    void Update()
    {
        if (!GlobalManager.recordingMode) //nested if statements 😍
        {
            if (Input.GetKey(key))
            {
                spriteRenderer.sprite = pressedSprite;

                var mainParticles = particleSys.main;
                foreach (GameObject note in GameObject.Find("SongManager").GetComponent<SongManager>().spawnedNotes)
                {
                    float distance = Vector2.Distance(note.transform.position, this.transform.position);

                    if (distance <= 1f)
                    {
                        Destroy(note);

                        if (distance <= 0.2f)
                        {
                            mainParticles.startColor = Color.cyan;
                            particleSys.Emit(10);
                            GlobalManager.currentScore += 5;
                            Debug.Log ("Perfect!");
                        }
                        else if (distance <= 0.5f)
                        {
                            mainParticles.startColor = Color.green;
                            particleSys.Emit(10);
                            GlobalManager.currentScore += 3;
                            Debug.Log ("Great!");
                        }
                        else if (distance <= 0.7f)
                        {
                            mainParticles.startColor = Color.yellow;
                            particleSys.Emit(10);
                            GlobalManager.currentScore += 1;
                            Debug.Log ("OK!");
                        }
                        else 
                        {
                            mainParticles.startColor = Color.red;
                            particleSys.Emit(10);
                            GlobalManager.currentScore += -1;
                            Debug.Log("Bad 🥺");
                        }
                    }
                }
            }
            else 
            {
                spriteRenderer.sprite = neutralSprite;
            }
        }
        else 
        {
            if (GlobalManager.recordingMode)
                {
                    if (Input.GetKeyDown(key))
                    {
                        songManager.currentMap.AddNoteAtTime(songManager.audioPlayer.time, Convert.ToInt32(noteLane.gameObject.name));
                    }
                }
        }
    }
}
