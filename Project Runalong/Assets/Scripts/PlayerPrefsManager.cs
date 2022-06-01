using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    // Score value initialized
    public const string Score = "Score";
    public static int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        //mileageTracker = GetComponent<MileageTracker>();
        score = PlayerPrefs.GetInt("Score");
        //mileage = reference to mileage tracker and trip mileage final score
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void UpdateScore()
    {
        PlayerPrefs.SetInt("Score", score);
        score = PlayerPrefs.GetInt("Score");
        PlayerPrefs.Save();
    }

    public static void IncreaseScore()
    {
        //milesOnDeath = mileageTracker.TripMileage.value;
        score += Convert.ToInt32(MileageTracker.tripMileage);
        UpdateScore();
    }

    public static void IncreaseScoreButton()
    {
        //milesOnDeath = mileageTracker.TripMileage.value;
        score += 10;
        UpdateScore();
    }
}
