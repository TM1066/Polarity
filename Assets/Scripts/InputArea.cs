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

                    if (distance <= 0.7f)
                    {
                        Destroy(note);

                        if (distance <= 0.1f)
                        {
                            mainParticles.startColor = Color.cyan;
                            if (!GlobalManager.lolzMode)
                            {
                                particleSys.Emit(10);
                            }
                            GlobalManager.currentScore += 5;
                            GlobalManager.currentCombo += 1;
                            Debug.Log ("Perfect!");
                        }
                        else if (distance <= 0.3f)
                        {
                            mainParticles.startColor = Color.green;
                            if (!GlobalManager.lolzMode)
                            {
                                particleSys.Emit(10);
                            }
                            GlobalManager.currentScore += 3;
                            GlobalManager.currentCombo += 1;
                            Debug.Log ("Great!");
                        }
                        else if (distance <= 0.5f)
                        {
                            mainParticles.startColor = Color.yellow;
                            if (!GlobalManager.lolzMode)
                            {
                                particleSys.Emit(10);
                            }
                            GlobalManager.currentScore += 1;
                            GlobalManager.currentCombo += 1;
                            Debug.Log ("OK!");
                        }
                        else 
                        {
                            mainParticles.startColor = Color.red;
                            if (!GlobalManager.lolzMode)
                            {
                                particleSys.Emit(10);
                            }
                            GlobalManager.currentScore += -1;
                            GlobalManager.currentCombo = 0;
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
        else if (GlobalManager.recordingMode)
        {
            if (Input.GetKeyDown(key))
            {
                songManager.currentMap.AddNoteAtBeat(songManager.GetCurrentClosestHalfBeat(), Convert.ToInt32(noteLane.gameObject.name));
                songManager.currentMap.SaveChanges();
            }
        }
    }
}
