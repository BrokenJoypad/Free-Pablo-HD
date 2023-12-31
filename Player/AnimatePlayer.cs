using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour
{
    private Player player;
    private Animator animator;

    private bool PlayerIsMoving = false;
    private bool PlayerIsAttacking = true;

    void Start()
    {
        player = Player.Instance;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // For the sake of readability...
        PlayerIsMoving = player.ReturnPlayerMoveState();
        PlayerIsAttacking = player.ReturnPlayerAttackState();

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
