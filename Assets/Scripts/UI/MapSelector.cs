using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MapSelector : MonoBehaviour
{
    public GameObject mapDisplayObjectPrefab;
    public List<GameObject> mapDisplayObjects = new List<GameObject>();
    //public Verticl

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Populate list of available Maps - better way of doing this, stop using dumb GameTracker!!!
        foreach (var map in GameObject.Find("Game Tracker").GetComponent<GameTracker>().GetListOfAvailableMaps())
        {
            var mapDisplayObject = Instantiate(mapDisplayObjectPrefab, this.transform);
            mapDisplayObject.GetComponent<MapDisplayObject>().mapToDisplay = map;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
