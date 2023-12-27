using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour
{
    private PlayerState playerState;
    private Animator animator;

    private bool PlayerIsMoving = false;
    private bool PlayerIsAttacking = true;

    void Start()
    {
        playerState = GetComponent<PlayerState>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerIsMoving = playerState.IsPlayerMoving();
        PlayerIsAttacking = playerState.IsPlayerAttacking();

        if(PlayerIsMoving){
            animator.SetBool("isMoving", true);
        } else{
            animator.SetBool("isMoving", false);
        }

        if(PlayerIsAttacking){
            animator.SetBool("isAttacking", true);
        } else{
            animator.SetBool("isAttacking", false);
        }
    }


}
