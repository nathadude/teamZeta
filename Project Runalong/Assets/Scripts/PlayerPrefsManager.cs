using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    // Score value initialized
    public const string Score = "Score";
    public static int score = 0;

    public const string MountUnlocked = "MountUnlocked";
    public const string OceanUnlocked = "OceanUnlocked";
    public static int OUnlock = 0;
    public static int MUnlock = 0;

    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("Score");
        MUnlock = PlayerPrefs.GetInt("MountUnlocked");
        OUnlock = PlayerPrefs.GetInt("OceanUnlocked");
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
        Debug.Log(score);
    }

    public static void BuyUpdateScore()
    {
        score = PlayerPrefs.GetInt("Score");
        PlayerPrefs.Save();
        Debug.Log(score);
    }

    public static void IncreaseScore()
    {
        //milesOnDeath = mileageTracker.TripMileage.value;
        score += Convert.ToInt32(MileageTracker.tripMileage);
        UpdateScore();
    }

    public static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void GiveMiles()
    {
        score += 500;
        UpdateScore();
    }

    public static void ResetMiles()
    {
        score = 0;
        UpdateScore();
    }

    //public static void TryBuy()
    //{
    //    //try to subtract cost from miles

    //    //catch errors

    //    //UpdateScore();
    //}
}
