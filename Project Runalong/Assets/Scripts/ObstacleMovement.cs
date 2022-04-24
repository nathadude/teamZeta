using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public enum MOVE_STATES {left, right};
    public MOVE_STATES currState;
    public float leftDuration;
    public float rightDuration;
    public float obstacleSpeed;

    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = new Timer(leftDuration, rightDuration);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.RunOnAlarm(Time.deltaTime))
        {
            if (currState == MOVE_STATES.left)
            {
                timer.SetAlarm(rightDuration);
                currState = MOVE_STATES.right;
            }
            else
            {
                timer.SetAlarm(leftDuration);
                currState = MOVE_STATES.left;
            }
        }

        if (currState == MOVE_STATES.left)
        {
            transform.Translate(Vector3.left * obstacleSpeed * Time.deltaTime);
        }
        else transform.Translate(Vector3.right * obstacleSpeed * Time.deltaTime);
    }
}
