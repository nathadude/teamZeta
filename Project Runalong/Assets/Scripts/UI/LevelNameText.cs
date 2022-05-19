using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LevelNameText : MonoBehaviour
{
    public IntSO levelID;
    private TextMeshProUGUI text;

    Dictionary<int, string> levelMappings = new Dictionary<int, string>(){
    {-1, "Test"},
    {0, "Placeholder"},
    {1, "Forest"}
};
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "Lvl: " + levelMappings[levelID.value];
    }
}
