using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using mixpanel;

public class DeathManager : MonoBehaviour
{
    public BoolSO GameOver;
    public FloatSO Mileage;
    public CanvasGroup DeathPanel;
    public TextMeshProUGUI MileageText;

    private bool gameOvered;

    // Start is called before the first frame update
    void Start()
    {
        GameOver.value = false;
        DeathPanel.interactable = false;
        DeathPanel.blocksRaycasts = false;
        DeathPanel.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver.value && !gameOvered)
        {
            gameOvered = true;

            MileageText.text = "Distance Traveled: " + Mileage.value.ToString("0.00");

            DeathPanel.interactable = true;
            DeathPanel.blocksRaycasts = true;
            DeathPanel.alpha = 1;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
