using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ramps up speed linearly: This function could be changed to a non-linear one
public class SpeedIncreaser : MonoBehaviour
{
    public FloatSO MoveSpeed;
    public BoolSO GameOver;
    public IntSO LevelID;

    public float initialSpeed;
    public float speedIncreasePerSecond;
    public float speedCap;

    public float initialSpeedHard;
    public float speedCapHard;

    // Start is called before the first frame update
    void Start()
    {
        if (LevelID.value == 3 || LevelID.value == -1)
        {
            MoveSpeed.value = initialSpeedHard;
        } else
        {
            MoveSpeed.value = initialSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver.value)
        {
            MoveSpeed.value = 0;
            return;
        }

        if ((LevelID.value == 3 || LevelID.value == -1) && MoveSpeed.value > speedCapHard) return;
        if (MoveSpeed.value > speedCap) return;
        MoveSpeed.value += Time.deltaTime * speedIncreasePerSecond;
    }
}
