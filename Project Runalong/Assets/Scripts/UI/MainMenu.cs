using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public CanvasGroup MainPanel;
    public CanvasGroup ControlPanel;
    public CanvasGroup LevelSelectPanel;

    private void Start()
    {
        ControlPanel.alpha = 0;
        ControlPanel.blocksRaycasts = false;
        ControlPanel.interactable = false;

        MainPanel.alpha = 1;
        MainPanel.blocksRaycasts = true;
        MainPanel.interactable = true;

        LevelSelectPanel.alpha = 0;
        LevelSelectPanel.blocksRaycasts = false;
        LevelSelectPanel.interactable = false;
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

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting the game");
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
