using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class AnimateEnemy : MonoBehaviour
{

    private EnemyState enemyState;
    private Animator animator;
    private bool EnemyIsMoving;
    private bool EnemyIsSearching;
    // Start is called before the first frame update
    void Start()
    {
      enemyState = GetComponent<EnemyState>();  
      animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyIsMoving = enemyState.IsEnemyMoving();
        EnemyIsSearching = enemyState.IsEnemySearching();

        if(EnemyIsMoving){
            animator.SetBool("isMoving", true);
            animator.SetBool("isSearching", false);
        } else{
            animator.SetBool("isMoving", false);
        }

        if(EnemyIsSearching){
            animator.SetBool("isSearching", true);
            animator.SetBool("isMoving", false);
        } else{
            animator.SetBool("isSearching", false);
        }
    }
}
