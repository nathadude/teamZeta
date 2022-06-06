
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public IntSO levelId;
    public BoolSO debug;
    public Animator CircleAC;
    private string levelToLoad;

    public GameObject StartButton;
    public GameObject BuyMountain;
    public GameObject BuyOcean;

/*    [Space]
    public Button TestButton;
    public GameObject TestBG;
    [Space]
    public Button PlaceholderButton;
    public GameObject PlaceholderBG;*/

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
        buttons = new Button[] {ForestButton, MountainButton, OceanButton};
        backgrounds = new GameObject[] {ForestBG, MountainBG, OceanBG};
        mountainUnlocked = PlayerPrefs.GetInt("MountUnlocked");
        oceanUnlocked = PlayerPrefs.GetInt("OceanUnlocked");
    }

    private void Start()
    {
        StartButton.SetActive(false);
        BuyMountain.SetActive(false);
        BuyOcean.SetActive(false);

        if (mountainUnlocked == 1)
        {
            MountainButton.GetComponentInChildren<TextMeshProUGUI>().text = "MOUNTAIN";
        } else
        {
            MountainButton.GetComponentInChildren<TextMeshProUGUI>().text = "LOCKED [100]";
        }

        if (oceanUnlocked == 1)
        {
            OceanButton.GetComponentInChildren<TextMeshProUGUI>().text = "OCEAN";
        }
        else
        {
            OceanButton.GetComponentInChildren<TextMeshProUGUI>().text = "LOCKED [250]";
        }

    }

    private void Update()
    {
        if (debug.value)
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
                Debug.Log("Prefs cleared");
            }
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
        PrepLevel(1, "LyraTest", 0);
    }

    public void PrepMountainLevel()
    {
        PrepLevel(2, "LyraTest", 1);
    }

    public void PrepOceanLevel()
    {
        PrepLevel(3, "LyraTest", 2);
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

            AudioManager.instance.Play("MenuSuccess");
            MountainButton.GetComponentInChildren<TextMeshProUGUI>().text = "MOUNTAIN";
            PrepMountainLevel();
        }
        //If not then dont subtract and play error sound
        else
        {
            AudioManager.instance.Play("MenuFail");
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

            AudioManager.instance.Play("MenuSuccess");
            OceanButton.GetComponentInChildren<TextMeshProUGUI>().text = "OCEAN";
            PrepOceanLevel();
        }
        //If not then dont subtract and re-run prepLevel
        else
        {
            AudioManager.instance.Play("MenuFail");
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
