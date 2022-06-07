using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    public TextMeshProUGUI ReadyToGo;
    public CanvasGroup DialoguePanel;
    public Animator FadeOutAC;
    public Animator CameraAC;

    private TextMeshProUGUI dialogue;
    private bool cutsceneOver = false;

    // Play the opening cutscene while loading the next scene async.
    // When next scene is ready allow player to advance by pressing any key

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.CrossfadeMusic("Birdsong", 0.25f);
        AudioManager.instance.PlayStoppableTrack("CarAmbient");
        DialoguePanel.alpha = 0;
        dialogue = DialoguePanel.GetComponentInChildren<TextMeshProUGUI>();
        dialogue.text = "";

        StartCoroutine(PrepTitle());
        StartCoroutine(PlayCutscene());
        ReadyToGo.enabled = false;
    }

    IEnumerator PrepTitle()
    {
        AsyncOperation sceneLoad = SceneManager.LoadSceneAsync("Title");
        sceneLoad.allowSceneActivation = false;

        while (!sceneLoad.isDone)
        {
            if (sceneLoad.progress >= 0.9f)
            {
                // Allow scene activation if player hits anything OR cutscene is over. Show the ready text
                ReadyToGo.enabled = true;
                if (cutsceneOver || Input.anyKey)
                {
                    AudioManager.instance.FadeOutStoppableTrack(0.5f);
                    sceneLoad.allowSceneActivation = true;
                }
            }
            yield return null;
        }

        // After next scene is loaded, then unload this scene
        SceneManager.UnloadSceneAsync("IntroCutscene");
    }

    IEnumerator PlayCutscene()
    {
        yield return new WaitForSeconds(2);
        StartCoroutine(FadeIn());
        dialogue.text = "The family roadtrip...";
        yield return new WaitForSeconds(3);

        yield return StartCoroutine(FadeOut());
        yield return new WaitForSeconds(1);

        StartCoroutine(FadeIn());
        dialogue.text = "Your parents made you leave behind your beloved pet dog.";
        yield return new WaitForSeconds(3);

        yield return StartCoroutine(FadeOut());
        yield return new WaitForSeconds(1);

        StartCoroutine(FadeIn());
        dialogue.text = "You stare out the window and imagine what they might be doing...";
        yield return new WaitForSeconds(3);

        StartCoroutine(FadeOut());
        AudioManager.instance.FadeOutMusic(3f);
        AudioManager.instance.FadeOutStoppableTrack(3f);
        FadeOutAC.SetTrigger("FadeOut");
        CameraAC.SetTrigger("FadeOut");
        yield return new WaitForSeconds(3f);

        cutsceneOver = true;
    }

    IEnumerator FadeIn()
    {
        while (DialoguePanel.alpha < 1)
        {
            DialoguePanel.alpha += Time.deltaTime;
            yield return null;
        }
        DialoguePanel.alpha = 1;
    }

    IEnumerator FadeOut()
    {
        while (DialoguePanel.alpha > 0)
        {
            DialoguePanel.alpha -= Time.deltaTime;
            yield return null;
        }
        DialoguePanel.alpha = 0;
    }
}
