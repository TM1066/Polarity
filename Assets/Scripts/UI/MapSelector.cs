using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapSelector : MonoBehaviour
{
    public GameObject mapDisplayObjectPrefab;
    public List<GameObject> mapDisplayObjects = new List<GameObject>();

    private int yOffset = 0;
    [SerializeField] int yOffsetIncrement = 300;
    //public Verticl

    public Image bgImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Populate list of available Maps - better way of doing this, stop using dumb GameTracker!!!
        foreach (var map in GameObject.Find("Game Tracker").GetComponent<GameTracker>().GetListOfAvailableMaps())
        {
            //mapdisplayobject mapduspklayobjectkaopsfk
            var mapDisplayObject = Instantiate(mapDisplayObjectPrefab, this.transform);
            mapDisplayObject.transform.localPosition = new Vector3(0, yOffset);

            mapDisplayObject.GetComponent<MapDisplayObject>().mapToDisplay = map;

            mapDisplayObjects.Add(mapDisplayObject);
            yOffset += yOffsetIncrement;
        }

        //EventSystem.current.SetSelectedGameObject(mapDisplayObjects[0]);
    }
    
    // private void OnSelectionChange()
    // {
    //     if (EventSystem.current.currentSelectedGameObject.TryGetComponent<MapDisplayObject>(out var mapDisplayObject))
        // {
        //     bgImage.sprite = mapDisplayObject.thumbnailImage.sprite;
        // }

    // }

    private void Update() //doing this every frame is dumb
    {
        if (EventSystem.current.currentSelectedGameObject.TryGetComponent<MapDisplayObject>(out var mapDisplayObject) != false)
        {
            bgImage.sprite = mapDisplayObject.thumbnailImage.sprite;
        }
    }
}
