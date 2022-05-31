using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ramps up speed linearly: This function could be changed to a non-linear one
public class SpeedIncreaser : MonoBehaviour
{
    public FloatSO MoveSpeed;
    public BoolSO GameOver;

    public float initialSpeed;
    public float speedIncreasePerSecond;
    public float speedCap;

    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed.value = initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver.value)
        {
            MoveSpeed.value = 0;
            return;
        }
        if (MoveSpeed.value > speedCap) return;
        MoveSpeed.value += Time.deltaTime * speedIncreasePerSecond;
    }
}
