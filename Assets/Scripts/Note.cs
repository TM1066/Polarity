using UnityEngine;

public class Note : MonoBehaviour
{
    public float despawnHeight;

    private SongManager songManager;

    void Start()
    {
        songManager = GameObject.Find("SongManager").GetComponent<SongManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y <= despawnHeight)
        {
            songManager.spawnedNotes.Remove(this.gameObject); 
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy() 
    {
        songManager.spawnedNotes.Remove(this.gameObject);    
    }
}
