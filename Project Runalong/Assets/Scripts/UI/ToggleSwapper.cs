using UnityEngine;
using TMPro;

public class ToggleSwapper : MonoBehaviour
{
    public BoolSO HoldToggle;
    public TextMeshProUGUI ToggleStatus;

    private void Start()
    {
        setText();
    }

    public void SwapToggle()
    {
        HoldToggle.value = !HoldToggle.value;
        setText();
    }

    private void setText()
    {
        if (HoldToggle.value)
        {
            ToggleStatus.text = "Status: Enabled";
            ToggleStatus.color = Color.green;
        }
        else
        {
            ToggleStatus.text = "Status: Disabled";
            ToggleStatus.color = Color.red;
        }
    }
}
