using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarWarning : MonoBehaviour
{
    public Image WarningSign;
    public BoolSO GameOver;

    private void Start()
    {
        WarningSign.enabled = false;
    }

    // If the car enters this region, play the warning sound. THen as the car passes play the car passing sound
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameOver.value) return;
        if (collision.tag == "Car")
        {
            AudioManager.instance.Play("CarWarning");
            StartCoroutine(flashWarning(0.4f));
        }
    }

    IEnumerator flashWarning(float time)
    {
        float startTime = Time.time;
        while (Time.time < startTime + time)
        {
            WarningSign.enabled = !WarningSign.enabled;
            yield return new WaitForSeconds(0.2f);
        }
        WarningSign.enabled = false;
        AudioManager.instance.Play("CarDrive");
    }
}
