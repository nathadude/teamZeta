using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    // Score value initialized
    public const string Score = "Score";
    public static int score = 0;
    public GameObject mileage;

    // Start is called before the first frame update
    void Start()
    {
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
        score += 10; //place holder, we want to eventually add final score of the round, at death
    }
}
