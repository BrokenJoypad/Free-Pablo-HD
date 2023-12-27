using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{

    [SerializeField] private bool PlayerMoving = false;
    [SerializeField] private bool PlayerAttacking = false;
    
    private IsPlayerAttacking isPlayerAttacking;
    private Vector3 LastPlayerPosition;

    void Start(){
        isPlayerAttacking = GetComponent<IsPlayerAttacking>();
    }

        void FixedUpdate()
    {
        LastPlayerPosition = transform.position;
    }

    void Update()
    {
        // Check If Player Is Moving
        if(LastPlayerPosition != transform.position){
            PlayerMoving = true;
        } else{
            PlayerMoving = false;
        }

        // Check If Player is Attacking
        PlayerAttacking = isPlayerAttacking.ReturnPlayerAttacking();

    }



    // These variables here are used for updating the current player state in the AnimatePlayer Script.
    public bool IsPlayerMoving(){
        return PlayerMoving;
    }

    public bool IsPlayerAttacking(){
        return PlayerAttacking;
    }
    
}
