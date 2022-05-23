using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

// For manually inserting values
public class LeaderboardController : MonoBehaviour
{
    public TMP_InputField Initials, PlayerScore;
    public DisplayLeaderboard displayLeaderboard;
    public int TargetLeaderboardId;


    public void SubmitScore()
    {
        LootLockerSDKManager.SubmitScore(Initials.text.ToUpper(), int.Parse(PlayerScore.text), LevelMappings.IdToLeaderboard[TargetLeaderboardId], (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully submitted score");
                // TODO: STUFF
            }
            else
            {
                Debug.Log("Failed to submit");
            }
        });
    }

}
