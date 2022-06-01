using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mixpanel;

// Given the LevelID passed, picks a folder from Resources and uses those prefabs to generate levels endlessly
public class LevelLoader : MonoBehaviour
{
    public IntSO LevelID;
    
    [Space]
    // Data for spawning player
    public GameObject PlayerPrefab;
    public Vector2 SpawnPoint; // Where player spawns

    [Space]
    // Data for spawning level chunks
    public Transform LevelPiecesParent;
    public float LevelPieceSpacing; // How wide a level piece is

    // Private data for tracking next spawn locations
    private GameObject playerInstance;
    private Vector3 nextPieceLocation; // Where the next piece will be
    private Vector3 nextPieceSpacing;
    private float loadNextLocation; // When to start loading the next piece

    // This list of levelpieces, populated at runtime
    private GameObject[] levelPieces;
    private GameObject startPiece;
    private GameObject backgroundContainer;

    void Start()
    {
        // Load levelPieces and background based on the LevelType
        switch(LevelID.value)
        {
            case -1: // Test
                levelPieces = Resources.LoadAll<GameObject>("Test");
                startPiece = Resources.Load<GameObject>("Start/ground1");
                backgroundContainer = Resources.Load<GameObject>("Backgrounds/MountainBG");
                break;
            case 0: // Placeholder
                levelPieces = Resources.LoadAll<GameObject>("Placeholder");
                startPiece = Resources.Load<GameObject>("Start/ground1");
                backgroundContainer = Resources.Load<GameObject>("Backgrounds/ForestBG");
                AudioManager.instance.PlayMusicIfNotPlaying("Mountain");
                break;
            case 1: // Forest
                levelPieces = Resources.LoadAll<GameObject>("Forest");
                startPiece = Resources.Load<GameObject>("Start/ground1");
                backgroundContainer = Resources.Load<GameObject>("Backgrounds/ForestBG");
                AudioManager.instance.PlayMusicIfNotPlaying("Forest");
                break;
            default:
                Debug.LogError("Error: No level pieces found for LevelType " + LevelID.value);
                break;
        }

        Debug.Log("Loaded " + levelPieces.Length + " Level pieces");

        // Load in background and attach to main camera
        GameObject mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        Instantiate(backgroundContainer, mainCam.transform);

        nextPieceLocation = new Vector3(0, -1, 0);
        nextPieceSpacing = new Vector3(LevelPieceSpacing, 0, 0);

        // Spawn the first 2 pieces. First one will be the default piece, next is tutorial piece if forest, random otherwise
        spawnPiece(startPiece);
        if (LevelID.value == 0 || LevelID.value == 1)
        {
            GameObject tutorialPiece = Resources.Load<GameObject>("Tutorial/T1");
            spawnPiece(tutorialPiece);
        } else
        {
            spawnNextPiece();
        }

        loadNextLocation = LevelPieceSpacing / 2;

        // Spawn player
        playerInstance = Instantiate(PlayerPrefab, SpawnPoint, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInstance != null)
        {
            if (playerInstance.transform.position.x > loadNextLocation)
            {
                spawnNextPiece();
            }
        }
    }

    // Generates the next level piece. Pass an index to pick a specific one, -1 for random
    private void spawnNextPiece(int idx = -1)
    {
        // Pick a random piece
        if (idx < 0)
            idx = Random.Range(0, levelPieces.Length);

        spawnPiece(levelPieces[idx]);
    }

    // Spawns a specific piece
    private void spawnPiece(GameObject piece)
    {
        // Instantiate it at the right position
        GameObject nextPiece = Instantiate(piece, LevelPiecesParent);
        nextPiece.transform.position = nextPieceLocation;

        // If more than 3 pieces, destroy the earliest one
        if (LevelPiecesParent.childCount > 3)
        {
            Destroy(LevelPiecesParent.GetChild(0).gameObject);
        }

        // Set the next spawn location and loading zone
        nextPieceLocation += nextPieceSpacing;
        loadNextLocation += LevelPieceSpacing;
    }
}
