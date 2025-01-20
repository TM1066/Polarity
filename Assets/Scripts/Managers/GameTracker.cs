using System.Collections.Generic;
using UnityEngine;

public class GameTracker : MonoBehaviour
{
    private static GameTracker instance = null;

    [SerializeField] List<Map> maps = new List<Map>();

    void Awake(){
        if (instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate
        }
    }
    public List<Map> GetListOfAvailableMaps()
    {
        return maps;
    }
}
