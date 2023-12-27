using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class IsPlayerAttacking : MonoBehaviour
{
 
    [Header("States")]
    [SerializeField] private bool hasTarget = false;
    [SerializeField] private bool isAttackingTarget = false;

    private GameCursor gameCursor;
    private Vector3 currentPosition;
    private Vector3 EnemyPosition;
    private Vector3 DistanceFromTarget;
    private GameObject Enemy;

    private int EnemyLayerMask;
    private int HitLayerMask;

    void Awake(){
        gameCursor = GameCursor.Instance;
        EnemyLayerMask = LayerMask.NameToLayer("Enemies");
    }

    void Update(){
        currentPosition = transform.position;
        waitForEnemyTarget();
        MoveTowardsTarget();
        AttackTarget();
    }

    // ========================================================================================== FUNCTIONS


    // ================ Wait until we click on a target on the Enemy Layer Mask
    void waitForEnemyTarget(){
        if(Input.GetButtonDown("LClick")){
            HitLayerMask = gameCursor.GetHitGameObject().layer;
            Enemy = gameCursor.GetHitGameObject();

            if(HitLayerMask == EnemyLayerMask){
                hasTarget = true;
            } else{
                Enemy = null;
                hasTarget = false;
            }
        }
    }

    // =============== If we have clicked on an Enemy, Move towards it.

    void MoveTowardsTarget(){
        if(hasTarget == true){
            EnemyPosition = Enemy.transform.position;
            DistanceFromTarget = currentPosition - EnemyPosition;
        }
    }

    // ============== Once we get there, attack it.

    void AttackTarget(){
    if(hasTarget == true && DistanceFromTarget.magnitude < 2f ){
            isAttackingTarget = true;
    } else{
            isAttackingTarget = false;
    }

    }
    // ============= Make it public knowledge that we're attacking
    public bool ReturnPlayerAttacking(){
        return isAttackingTarget;
    }

    public GameObject ReturnPlayerTarget(){
        return Enemy;
    }
}

