using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mixpanel;

public class SessionLengthLogging : MonoBehaviour
{
    private void Awake()
    {
        // Start session, dontdestroy
        Mixpanel.Track("Session start");
        DontDestroyOnLoad(gameObject);
        Mixpanel.StartTimedEvent("Session Length");
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Session over, logging length");
        // Log session over
        Mixpanel.Track("Session Length");
    }
}
