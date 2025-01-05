using UnityEngine;

public class Note : MonoBehaviour
{
    public float despawnHeight;

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(transform.position.y, despawnHeight))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy() 
    {
        GameObject.Find("SongManager").GetComponent<SongManager>().spawnedNotes.Remove(this.gameObject);    
    }
}
