using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public enum MOVE_STATES {left, right, up, down};
    public bool moveVertical;

    public float negDuration;
    public float posDuration;
    public float obstacleSpeed;
    
    private MOVE_STATES currState;
    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = new Timer(negDuration, posDuration);
        currState = moveVertical ? MOVE_STATES.down : MOVE_STATES.left;
    }

    // Update is called once per frame
    void Update()
    {
        // Move in one direction complete: Switch
        if (timer.RunOnAlarm(Time.deltaTime))
        {
            if (!moveVertical)
            {
                if (currState == MOVE_STATES.left)
                {
                    timer.SetAlarm(posDuration);
                    currState = MOVE_STATES.right;
                }
                else
                {
                    timer.SetAlarm(negDuration);
                    currState = MOVE_STATES.left;
                }
            } else
            {
                if (currState == MOVE_STATES.down)
                {
                    timer.SetAlarm(posDuration);
                    currState = MOVE_STATES.up;
                }
                else
                {
                    timer.SetAlarm(negDuration);
                    currState = MOVE_STATES.down;
                }
            }
        }

        // Translate depending on move_state and move direction
        Vector2 move = Vector2.zero;
        switch (currState)
        {
            case MOVE_STATES.left:
                move = Vector2.left;
                break;
            case MOVE_STATES.right:
                move = Vector2.right;
                break;
            case MOVE_STATES.up:
                move = Vector2.up;
                break;
            case MOVE_STATES.down:
                move = Vector2.down;
                break;
            default:
                Debug.LogWarning("Invalid MOVE_STATE in ObstacleMovement");
                break;
        }

        transform.Translate(move * obstacleSpeed * Time.deltaTime);
    }
}
