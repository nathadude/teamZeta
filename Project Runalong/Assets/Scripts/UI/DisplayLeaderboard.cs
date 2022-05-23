using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;
using TMPro;

public class DisplayLeaderboard : MonoBehaviour
{
    public bool ShowOnStartup;
    public TextMeshProUGUI[] Entries;
    public TextMeshProUGUI LevelTitle;
    public Button NextButton;
    public Button PrevButton;
    private int currentLeaderboard = -1; // For main menu display

    private void Start()
    {
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully started Leaderboard Session");
                if (ShowOnStartup) ShowScores(10);
            }
            else
            {
                Debug.Log("Failed to create session");
            }
        });

        if (PrevButton != null)
        {
            PrevButton.interactable = false;
        }
    }

    // Shows the next leaderboard, on the main menu
    public void NextLeaderboard()
    {
        // Adjust index
        currentLeaderboard++;

        // Disable buttons if at end of a list
        if (!LevelMappings.Levels.Contains(currentLeaderboard + 1))
        {
            NextButton.interactable = false;
        }
        PrevButton.interactable = true;

        // Display
        ShowScores(10);
    }

    // Shows the previous leaderboard, on the main menu
    public void PrevLeaderboard()
    {
        // Adjust index
        currentLeaderboard--;

        // Disable buttons if at end of a list
        if (!LevelMappings.Levels.Contains(currentLeaderboard - 1))
        {
            PrevButton.interactable = false;
        }
        NextButton.interactable = true;

        // Display
        ShowScores(10);
    }

    public void ShowLevelScores(IntSO levelIdx)
    {
        currentLeaderboard = levelIdx.value;
        ShowScores(10);
    }

    // Fills up the leaderboard text based on current leaderboard
    private void ShowScores(int count)
    {
        LootLockerSDKManager.GetScoreList(LevelMappings.IdToLeaderboard[currentLeaderboard], count, (response) =>
        {
            if (response.success)
            {
                LootLockerLeaderboardMember[] scores = response.items;

                for (int i = 0; i < scores.Length; i++)
                {
                    string milesText = (scores[i].score / 100f).ToString("0.00");
                    Entries[i].text = (i + 1 + ") " + scores[i].member_id + ":     " + milesText);
                }


                // If not all leaderboard slots would be filled, empty the text
                if (scores.Length < count)
                {
                    for (int i = scores.Length; i < count; i++)
                    {
                        Entries[i].text = "";
                    }
                }

                LevelTitle.text = "LVL: " + LevelMappings.IdToName[currentLeaderboard];
            }
            else
            {
                Debug.Log("Failed to get scores");
                LevelTitle.text = "ERROR";
            }
        });
    }
}
