using UnityEngine;
using TMPro;

public class ComboText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI comboTextMesh;

    // Update is called once per frame
    void Update(){
        if (GlobalManager.currentCombo > 0){
            comboTextMesh.text = $"Combo: {GlobalManager.currentCombo}";
        }
        else {
            comboTextMesh.text = "";
        }
    }
}
