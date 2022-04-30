using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MileageUI : MonoBehaviour
{
    public TextMeshProUGUI MileageDisplay;
    public TextMeshProUGUI HiScoreDisplay;

    public FloatSO Mileage;
    public FloatSO HiScore;

    // Update is called once per frame
    void Update()
    {
        MileageDisplay.text = Mileage.value.ToString("0000.00");
        HiScoreDisplay.text = Mathf.Floor(HiScore.value).ToString("0000");
    }
}
