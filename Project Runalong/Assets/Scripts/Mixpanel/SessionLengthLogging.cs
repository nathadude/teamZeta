using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mixpanel;

public class SessionLengthLogging : MonoBehaviour
{
    static SessionLengthLogging instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Already present - destroying");
            Destroy(gameObject);
            return;
        } else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
        // Start session, dontdestroy
        Debug.Log("MIXPANEL: Logging Session Start and Start Session Timer");
        Mixpanel.Track("Session start");
        Mixpanel.StartTimedEvent("Session Length");
    }

    private void OnApplicationQuit()
    {
        Debug.Log("MIXPANEL: Logging Session End");
        // Log session over
        Mixpanel.Track("Session Length");
        Mixpanel.Flush();
    }

    private void OnApplicationFocus(bool focus)
    {
        Mixpanel.Flush();
    }
}
