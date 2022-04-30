using UnityEngine;
using mixpanel;

// Runs are started in LevelLoader, we log the end of the run here though, only once per time the scene is loaded
public class RunLengthLogging : MonoBehaviour
{
    public BoolSO GameOver;
    public IntSO LevelID;
    private bool logged;

    private void Start()
    {
        // Log Run Start
        Debug.Log("MIXPANEL: Starting Run Timer");
        Mixpanel.StartTimedEvent("Run Length");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver.value && !logged)
        {
            Debug.Log("MIXPANEL: Ending Run Timer");
            var props = new Value();
            props["Level ID"] = LevelID.value;
            Mixpanel.Track("Run Length", props);
            logged = true;
        }
        
    }
}
