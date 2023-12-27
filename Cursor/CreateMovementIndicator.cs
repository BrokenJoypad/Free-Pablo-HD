using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMovementIndicator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject movePrefab;

    [Header("Variables")]
    [SerializeField] private bool IndicatorSpawned = false;

    private Vector3 indicatorSpawnPosition;
    private GameObject newMovePrefab;
    private GameCursor gameCursor;
    private GameObject playerTarget;
    private IsPlayerAttacking isPlayerAttacking;

    void Start()
    {
        gameCursor = GameCursor.Instance;
        isPlayerAttacking = Player.GetComponent<IsPlayerAttacking>();
    }

    void Update()
    {
        playerTarget = isPlayerAttacking.ReturnPlayerTarget();

        if (Input.GetButton("LClick"))
        {
            if (playerTarget != null)
            {
                indicatorSpawnPosition = playerTarget.transform.position;
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

        if (Vector3.Distance(Player.transform.position, indicatorSpawnPosition) < 0.5f)
        {
            if (IndicatorSpawned)
            {
                Destroy(newMovePrefab);
                IndicatorSpawned = false;
            }
        }
    }
}
