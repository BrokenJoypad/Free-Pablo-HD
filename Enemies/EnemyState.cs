using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{

    [SerializeField] private bool EnemyMoving;
    [SerializeField] private bool EnemySearching;

     private EnemyBehaviour EnemyBehaviour;
    void Start()
    {
        EnemyBehaviour = GetComponent<EnemyBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {

        // Check All Possible Scripts that could mean the enemy is moving around.
        if (EnemyBehaviour.returnMoveState() == true){
            EnemyMoving = true;
        } else{
            EnemyMoving = false;
        }

        if(EnemyBehaviour.returnSearchState() == true){
            EnemySearching = true;
        } else{
            EnemySearching = false;
        }
    }

    public bool IsEnemyMoving(){
        return EnemyMoving;
    }

        public bool IsEnemySearching(){
        return EnemySearching;
    }
}
