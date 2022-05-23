using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using LootLocker.Requests;

public class DeathManager : MonoBehaviour
{
    public BoolSO GameOver;
    public FloatSO Mileage;
    public IntSO LevelID;

    [Space]
    public CanvasGroup DeathPanel; // Main parent panel
    public CanvasGroup CompletePanel; // For retry/main menu
    public CanvasGroup LeaderboardPanel; // For leaderboard stuff
    public CanvasGroup SubmissionPanel; // Leaderboard subpanel
    public CanvasGroup LBCompletePanel; // Leaderboard complete

    private CanvasGroup[] AllPanels;

    [Space]
    public TextMeshProUGUI VerifyingText;
    public TMP_InputField InitialsInput; // Initials Input field
    public TextMeshProUGUI MileageText;
    public DisplayLeaderboard displayLeaderboard;

    private bool gameOvered;
    private float mileageAtEnd;

    // Start is called before the first frame update
    void Start()
    {
        GameOver.value = false;

        AllPanels = new CanvasGroup[]
        {
            DeathPanel,
            CompletePanel,
            LeaderboardPanel,
            SubmissionPanel,
            LBCompletePanel
        };

        HideAllPanels(AllPanels);
        TogglePanel(SubmissionPanel); // This is the only visible panel at start
    }

    // Update is called once per frame
    void Update()
    {
        // Gameover: Set DeathPanel and do the CheckScore
        if (GameOver.value && !gameOvered)
        {
            gameOvered = true;

            MileageText.text = "Distance Traveled: " + Mileage.value.ToString("0.00");
            mileageAtEnd = Mileage.value;

            TogglePanel(DeathPanel);

            CheckScore();
        }
    }

    // Check the player's mileage to see if it's in the top 10.
    // Show the score prompt if yes, show regular menu otherwise
    private void CheckScore()
    {
        LootLockerSDKManager.GetScoreList(LevelMappings.IdToLeaderboard[LevelID.value], 10, (response) =>
        {
            if (response.success)
            {
                VerifyingText.enabled = false;
                // Check the last one on the list, if yours is better then time to input a score
                LootLockerLeaderboardMember[] scores = response.items;
                Debug.Log("Lowest score: " + scores[scores.Length - 1].score);
                Debug.Log("User score: " + mileageAtEnd * 100);
                if (scores.Length < 10 || mileageAtEnd * 100 > scores[scores.Length - 1].score)
                {
                    Debug.Log("Score was better!");
                    TogglePanel(LeaderboardPanel);
                } else
                {
                    Debug.Log("Score was not better");
                    TogglePanel(CompletePanel);
                }
            }
            else
            {
                Debug.Log("Failed to check");
            }
        });
    }

    // For submitting a level high score
    public void SubmitScore()
    {
        LootLockerSDKManager.SubmitScore(InitialsInput.text.ToUpper(), Mathf.RoundToInt(Mileage.value * 100), LevelMappings.IdToLeaderboard[LevelID.value], (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully submitted score");
                displayLeaderboard.ShowLevelScores(LevelID);

                TogglePanel(SubmissionPanel);
                TogglePanel(LBCompletePanel);
            }
            else
            {
                Debug.Log("Failed to submit");
            }
        });
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void TogglePanel(CanvasGroup g)
    {
        if (g.alpha == 1)
        {
            g.alpha = 0;
            g.blocksRaycasts = false;
            g.interactable = false;
        } else
        {
            g.alpha = 1;
            g.blocksRaycasts = true;
            g.interactable = true;
        }
    }

    private void HideAllPanels(CanvasGroup[] gs)
    {
        foreach(CanvasGroup g in gs)
        {
            g.alpha = 0;
            g.blocksRaycasts = false;
            g.interactable = false;
        }
    }
}
