using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class MapSelector : MonoBehaviour
{

    public GameObject mapDisplayObjectPrefab;
    public List<GameObject> mapDisplayObjects = new List<GameObject>();

    public float middleYInScene = 0;

    //public Verticl

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // taking care of y change making it fade

        foreach (GameObject displayObject in mapDisplayObjects) 
        {
            var textObjects = displayObject.GetComponentsInChildren<TextMeshProUGUI>();

            foreach (TextMeshProUGUI textObject in textObjects)
            {
                textObject.color = new Color (textObject.color.r, textObject.color.g, textObject.color.b, GetAlphaFromY (textObject.color.a, textObject.transform.position.y));
            }
        }
        
    }

    float GetAlphaFromY(float colorAlpha, float y)
    {
        // not sure about this until I can test it
        return colorAlpha - Mathf.Abs(middleYInScene - y);
    }
}
