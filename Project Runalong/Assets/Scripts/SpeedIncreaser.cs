using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ramps up speed linearly: This function could be changed to a non-linear one
public class SpeedIncreaser : MonoBehaviour
{
    public FloatSO MoveSpeed;
    public float initialSpeed;
    public float speedIncreasePerSecond;
    public float speedCap;

    private static bool noIncrease; // If the speed increase is too crazy
    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed.value = initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            noIncrease = !noIncrease;
        }

        if (noIncrease || MoveSpeed.value > speedCap) return;
        MoveSpeed.value += Time.deltaTime * speedIncreasePerSecond;
    }
}
