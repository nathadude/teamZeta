using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    public void PlayClickSound()
    {
        AudioManager.instance.Play("ButtonClick");
    }
}
