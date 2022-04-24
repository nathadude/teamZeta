using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Given the LevelType passed, picks a folder from Resources and uses those prefabs to generate levels endlessly
public class LevelLoader : MonoBehaviour
{
    public float LevelType = 0; // 0 = placeholder
    
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

    void Awake()
    {
        // Load levelPieces based on the LevelType
        switch(LevelType)
        {
            case 0: // Placeholder
                levelPieces = Resources.LoadAll<GameObject>("Placeholder");
                break;
            default:
                Debug.LogError("Error: No level pieces found for LevelType " + LevelType);
                break;
        }

        Debug.Log("Loaded " + levelPieces.Length + " Level pieces");

        nextPieceLocation = new Vector3(0, -1, 0);
        nextPieceSpacing = new Vector3(LevelPieceSpacing, 0, 0);

        // Spawn the first 2 pieces. First one will be the default piece, next random
        spawnNextPiece(0);
        spawnNextPiece();
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

        // Instantiate it at the right position
        GameObject nextPiece = Instantiate(levelPieces[idx], LevelPiecesParent);
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
