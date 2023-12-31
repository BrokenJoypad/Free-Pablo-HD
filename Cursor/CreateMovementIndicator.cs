using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMovementIndicator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player player;
    [SerializeField] private GameObject movePrefab;

    [Header("Variables")]
    [SerializeField] private bool IndicatorSpawned = false;

    private Vector3 indicatorSpawnPosition;
    private GameObject newMovePrefab;
    private GameCursor gameCursor;
    private GameObject playerTarget;

    void Start()
    {
        gameCursor = GameCursor.Instance;
        player = Player.Instance;
    }

    void Update()
    {
        playerTarget = gameCursor.GetHitGameObject();

        if (Input.GetButton("LClick"))
        {
            if (playerTarget != null && playerTarget.name != "Environment__Ground")
            {
                indicatorSpawnPosition = new Vector3(playerTarget.transform.position.x, 0, playerTarget.transform.position.z);
            }
            else
            {
                indicatorSpawnPosition = gameCursor.ReturnCursorPosition();
            }

            if (IndicatorSpawned)
            {
                Destroy(newMovePrefab);
            }

            newMovePrefab = Instantiate(movePrefab, indicatorSpawnPosition, Quaternion.identity);
            IndicatorSpawned = true;
        }

        if (Vector3.Distance(player.transform.position, indicatorSpawnPosition) < 0.5f)
        {
            if (IndicatorSpawned)
            {
                Destroy(newMovePrefab);
                IndicatorSpawned = false;
            }
        }
    }
}
