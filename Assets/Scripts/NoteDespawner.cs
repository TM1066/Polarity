using UnityEngine;

public class NoteDespawner : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Note"))
        {
            Destroy(other);
        }
    }

}
