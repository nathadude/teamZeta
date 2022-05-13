using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public BoolSO Paused;
    public BoolSO GameOver;
    public CanvasGroup PausePanel;
    private void Awake()
    {
        Paused.value = false;
        PausePanel.interactable = false;
        PausePanel.blocksRaycasts = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameOver.value)
        {
            pauseToggle();
        }
    }

    private void pauseToggle()
    {
        Paused.value = !Paused.value;
        if (Paused.value)
        {
            PausePanel.alpha = 1;
            Time.timeScale = 0;
            PausePanel.interactable = true;
            PausePanel.blocksRaycasts = true;
        } else
        {
            PausePanel.alpha = 0;
            Time.timeScale = 1;
            PausePanel.interactable = false;
            PausePanel.blocksRaycasts = false;
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
