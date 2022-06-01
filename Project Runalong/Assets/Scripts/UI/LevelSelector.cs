
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public IntSO levelId;
    private string levelToLoad;

    public GameObject StartButton;
    [Space]
    public Button TestButton;
    public GameObject TestBG;
    [Space]
    public Button PlaceholderButton;
    public GameObject PlaceholderBG;

    [Space]
    public Button ForestButton;
    public GameObject ForestBG;

    private Button[] buttons;
    private GameObject[] backgrounds;

    private void Awake()
    {
        buttons = new Button[] {TestButton, PlaceholderButton, ForestButton};
        backgrounds = new GameObject[] { TestBG, PlaceholderBG, ForestBG };
        StartButton.SetActive(false);
    }

    public void LoadLevel()
    {
        AudioManager.instance.StopMusic();
        SceneManager.LoadScene(levelToLoad);
    }

    public void PrepTestLevel()
    {
        PrepLevel(-1, "LyraTest", 0);
    }

    public void PrepPlaceholderLevel()
    {
        PrepLevel(0, "LyraTest", 1);
    }

    public void PrepForestLevel()
    {
        PrepLevel(1, "LyraTest", 2);
    }

    // Preps a level by setting active LevelID/Name, disabling the pressed button, and setting BG
    private void PrepLevel(int LevelID, string LevelName, int ButtonIdx)
    {
        StartButton.SetActive(true);

        levelId.value = LevelID;
        levelToLoad = LevelName;

        enableAllButtons();
        buttons[ButtonIdx].interactable = false;

        disableAllBackgrounds();
        backgrounds[ButtonIdx].SetActive(true);
    }

    private void enableAllButtons()
    {
        foreach (Button b in buttons)
        {
            b.interactable = true;
        }
    }

    private void disableAllBackgrounds()
    {
        foreach (GameObject g in backgrounds)
        {
            g.SetActive(false);
        }
    }
}
