using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class LeaderboardController : MonoBehaviour
{
    public TMP_InputField Initials, PlayerScore;
    public IntSO LevelID; // For level display

    public DisplayLeaderboard displayLeaderboard;

    // TODO: When level is complete, check leaderboard to see if player score can be submitted.  (THIS SHOULD BE ON DEATHPANEL)
    // If true, offer the prompt to submit score.
    // Otherwise, do not

    // For submitting a level high score
    public void SubmitScore()
    {
        LootLockerSDKManager.SubmitScore(Initials.text, int.Parse(PlayerScore.text), LevelMappings.IdToLeaderboard[LevelID.value], (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully submitted score");
            }
            else
            {
                Debug.Log("Failed to submit");
            }
        });
    }
}
