using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;
using TMPro;

public class LeaderboardController : MonoBehaviour
{
    public TMP_InputField Initials, PlayerScore;
    public int LeaderboardID;
    int EntryCount = 10;
    public TextMeshProUGUI[] Entries;

    private void Start()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully started session for ID " + LeaderboardID);
            } else
            {
                Debug.Log("Failed to create session for ID " + LeaderboardID);
            }
        });
    }

    public void SubmitScore()
    {
        LootLockerSDKManager.SubmitScore(Initials.text, int.Parse(PlayerScore.text), LeaderboardID, (response) =>
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

    public void ShowFiveScores()
    {
        ShowScores(5);
    }

    private void ShowScores(int count)
    {
        LootLockerSDKManager.GetScoreList(LeaderboardID, count, (response) =>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] scores = response.items;

                for (int i = 0; i < scores.Length; i++)
                {
                    Entries[i].text = (scores[i].member_id + ":     " + scores[i].score);
                }


                // If not all leaderboard slots would be filled, empty the text
                if (scores.Length < count)
                {
                    for (int i = scores.Length; i < count; i++)
                    {
                        Entries[i].text = "";
                    }
                }
            }
            else
            {
                Debug.Log("Failed to get scores");
            }
        });
    }
}
