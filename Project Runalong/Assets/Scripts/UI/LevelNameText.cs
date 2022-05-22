using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class LevelNameText : MonoBehaviour
{
    public IntSO levelID;
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.text = "Lvl: " + LevelMappings.IdToName[levelID.value];
    }
}
