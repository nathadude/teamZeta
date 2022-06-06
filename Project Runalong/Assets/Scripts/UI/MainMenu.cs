using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public CanvasGroup MainPanel;
    public CanvasGroup ControlPanel;
    public CanvasGroup LevelSelectPanel;
    public CanvasGroup LeaderboardPanel;
    public CanvasGroup ShopPanel;
    public CanvasGroup CreditsPanel;

    private CanvasGroup[] AllGroups;

    private void Awake()
    {
        AllGroups = new CanvasGroup[]{
            MainPanel,
            ControlPanel,
            LevelSelectPanel,
            LeaderboardPanel,
            ShopPanel,
            CreditsPanel
        };

        for (int i = 1; i < AllGroups.Length; i++)
        {
            AllGroups[i].alpha = 0;
            AllGroups[i].blocksRaycasts = false;
            AllGroups[i].interactable = false;
        }
        MainPanel.alpha = 1;
        MainPanel.blocksRaycasts = true;
        MainPanel.interactable = true;
    }

    private void Start()
    {
        AudioManager.instance.CrossfadeMusic("Title", 0.5f);
    }


    public void OpenControlPanel()
    {
        TogglePanel(MainPanel, ControlPanel);
    }

    public void CloseControlPanel()
    {
        TogglePanel(ControlPanel, MainPanel);
    }

    public void OpenLevelSelect()
    {
        TogglePanel(MainPanel, LevelSelectPanel);
    }

    public void CloseLevelSelect()
    {
        TogglePanel(LevelSelectPanel, MainPanel);
    }

    public void OpenLeaderboard()
    {
        TogglePanel(MainPanel, LeaderboardPanel);
    }

    public void CloseLeaderboard()
    {
        TogglePanel(LeaderboardPanel, MainPanel);
    }

    public void OpenCredits()
    {
        TogglePanel(MainPanel, CreditsPanel);
    }

    public void CloseCredits()
    {
        TogglePanel(CreditsPanel, MainPanel);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting the game");
    }

    public void OpenShop()
    {
        TogglePanel(MainPanel, ShopPanel);
    }

    public void CloseShopPanel()
    {
        TogglePanel(ShopPanel, MainPanel);
    }

    private void TogglePanel(CanvasGroup fadeOut, CanvasGroup fadeIn)
    {
        fadeOut.alpha = 0;
        fadeOut.blocksRaycasts = false;
        fadeOut.interactable = false;

        fadeIn.alpha = 1;
        fadeIn.blocksRaycasts = true;
        fadeIn.interactable = true;
    }
}
