using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    public BoolSO debug;
    string secretCode; // The secret code is zeta, if you type it debug starts
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            secretCode = "Z";
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (secretCode.CompareTo("Z") == 0) {
                secretCode = "ZE";
            } else
            {
                secretCode = "";
            }
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (secretCode.CompareTo("ZE") == 0)
            {
                secretCode = "ZET";
            }
            else
            {
                secretCode = "";
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (secretCode.CompareTo("ZET") == 0)
            {
                if (debug.value)
                {
                    debug.value = false;
                    AudioManager.instance.Play("MenuFail");
                } else
                {
                    debug.value = true;
                    AudioManager.instance.Play("MenuSuccess");
                }
            }
            secretCode = "";
        }
    }
}
