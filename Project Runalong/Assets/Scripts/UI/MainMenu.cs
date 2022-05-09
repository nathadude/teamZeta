using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public CanvasGroup MainPanel;
    public CanvasGroup ControlPanel;

    private void Start()
    {
        ControlPanel.alpha = 0;
        ControlPanel.blocksRaycasts = false;
        ControlPanel.interactable = false;

        MainPanel.alpha = 1;
        MainPanel.blocksRaycasts = true;
        MainPanel.interactable = true;
    }

    public void OpenControlPanel()
    {
        TogglePanel(MainPanel, ControlPanel);
    }

    public void CloseControlPanel()
    {
        TogglePanel(ControlPanel, MainPanel);
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
