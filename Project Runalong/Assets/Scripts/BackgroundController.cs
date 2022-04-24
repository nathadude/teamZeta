using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject NormalBackgroundContainer;
    public GameObject NormalBackground;
    public float NormalBGSpacing;
    public float NormalYOffset;

    [Space]
    public GameObject SlowBackgroundContainer;
    public GameObject SlowBackground;
    public float SlowBGSpacing;
    public float SlowYoffset;

    private Transform playerRef;
    private float normalNextLoadPosition;
    private Vector2 normalNextSpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player").transform;

        // Spawn the first 3 bgs, then set time for next spawn
        normalNextLoadPosition = NormalBGSpacing * -2f;
        normalNextSpawnPosition = new Vector2(0, NormalYOffset);
        for (int i = 0; i < 3; i++)
        {
            spawnNormalBackgroundPiece();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRef.position.x > normalNextLoadPosition)
        {
            spawnNormalBackgroundPiece();
        }
    }

    private void spawnNormalBackgroundPiece()
    {
        // Instantiate and parent it
        GameObject nextBG = Instantiate(NormalBackground, NormalBackgroundContainer.transform);
        nextBG.transform.position = normalNextSpawnPosition;


        // If more than 4 pieces, destroy the earliest one
        if (NormalBackgroundContainer.transform.childCount > 4)
        {
            Destroy(NormalBackgroundContainer.transform.GetChild(0).gameObject);
        }

        // Update next spawn position and load position
        normalNextSpawnPosition += new Vector2(NormalBGSpacing, 0);
        normalNextLoadPosition += NormalBGSpacing;
    }
}
