using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To be attached to player. Updates the FloatSO for current mileage based on transform position, and the High Score for the level
public class MileageTracker : MonoBehaviour
{
    public FloatSO TripMileage;
    public FloatSO HighScore;
    public bool ResetScore;
    public static float tripMileage; //float that updates and can be called, added and stored for the shop

    private float initialPos;
    private Transform playerRef;

    // Start is called before the first frame update
    void Awake()
    {
        playerRef = GetComponent<Transform>();
        initialPos = playerRef.position.x;
        TripMileage.value = 0;
        if (ResetScore) HighScore.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRef != null)
        {
            TripMileage.value = (playerRef.position.x - initialPos) / 100; // divided by 100 to approximate miles
            if (HighScore.value < TripMileage.value) HighScore.value = TripMileage.value;
            tripMileage = TripMileage.value;
        }
    }
}
