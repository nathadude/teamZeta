
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelSelector : MonoBehaviour
{
    public IntSO levelId;
    public Animator CircleAC;
    private string levelToLoad;

    public GameObject StartButton;
    public GameObject BuyMountain;
    public GameObject BuyOcean;

    [Space]
    public Button TestButton;
    public GameObject TestBG;
    [Space]
    public Button PlaceholderButton;
    public GameObject PlaceholderBG;

    [Space]
    public Button ForestButton;
    public GameObject ForestBG;

    [Space]
    public Button MountainButton;
    public GameObject MountainBG;

    [Space]
    public Button OceanButton;
    public GameObject OceanBG;

    private Button[] buttons;
    private GameObject[] backgrounds;

    private int mountainUnlocked;
    private int oceanUnlocked;
    private int mountainCost = 100;
    private int oceanCost = 250;

    private void Awake()
    {
        buttons = new Button[] {TestButton, PlaceholderButton, ForestButton, MountainButton, OceanButton};
        backgrounds = new GameObject[] { TestBG, PlaceholderBG, ForestBG, MountainBG, OceanBG};
        StartButton.SetActive(false);
        BuyMountain.SetActive(false);
        BuyOcean.SetActive(false);
        mountainUnlocked = PlayerPrefs.GetInt("MountUnlocked");
        oceanUnlocked = PlayerPrefs.GetInt("OceanUnlocked");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            PlayerPrefsManager.GiveMiles();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayerPrefsManager.ResetMiles();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayerPrefsManager.ClearPrefs();
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void LoadLevelAfterFade()
    {
        CircleAC.SetTrigger("FadeOut");
        StartCoroutine(loadSceneAfterDelay(0.5f));
    }

    IEnumerator loadSceneAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        LoadLevel();
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

    public void PrepMountainLevel()
    {
        PrepLevel(2, "LyraTest", 3);
    }

    public void PrepOceanLevel()
    {
        PrepLevel(3, "LyraTest", 4);
    }

    public void TryBuyMountain()
    {
        int tempScore = PlayerPrefs.GetInt("Score");
        //Check Miles
        //If valid amt of miles then subtract
        if (tempScore >= mountainCost)
        {
            //Subtract
            PlayerPrefs.SetInt("Score", tempScore - mountainCost);
            PlayerPrefsManager.BuyUpdateScore();
            Debug.Log(tempScore);
            //set bool to true/unlocked
            PlayerPrefs.SetInt("MountUnlocked", 1);
            PlayerPrefs.Save();
            mountainUnlocked = 1;

            PrepMountainLevel();
        }
        //If not then dont subtract and play error sound
        else
        {
            //AudioManager.instance.Play("Failure");
        }
    }

    public void TryBuyOcean()
    {
        int tempScore = PlayerPrefs.GetInt("Score");
        //Check Miles
        //If valid amt of miles then subtract
        if (tempScore >= oceanCost)
        {
            //Subtract                                                       
            PlayerPrefs.SetInt("Score", tempScore - oceanCost);
            PlayerPrefsManager.BuyUpdateScore();
            Debug.Log(tempScore);
            //set bool to true/unlocked
            PlayerPrefs.SetInt("OceanUnlocked", 1);
            PlayerPrefs.Save();
            oceanUnlocked = 1;

            PrepOceanLevel();
        }
        //If not then dont subtract and re-run prepLevel
        else
        {
            //AudioManager.instance.Play("Failure");
        }
    }

    // Preps a level by setting active LevelID/Name, disabling the pressed button, and setting BG
    private void PrepLevel(int LevelID, string LevelName, int ButtonIdx)
    {
        if (mountainUnlocked == 0 && LevelID == 2)
        {
            StartButton.SetActive(false);
            BuyOcean.SetActive(false);
            BuyMountain.SetActive(true);
        }
        else if (oceanUnlocked == 0 && LevelID == 3)
        {
            StartButton.SetActive(false);
            BuyMountain.SetActive(false);
            BuyOcean.SetActive(true);
        }
        else
        {
            BuyMountain.SetActive(false);
            BuyOcean.SetActive(false);
            StartButton.SetActive(true);
            levelId.value = LevelID;
            levelToLoad = LevelName;
        }

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
