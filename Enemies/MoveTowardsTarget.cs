using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour
{
    private CheckAreaCollider areaCollider;
    private EnemyStats EnemyStats;
    [SerializeField] private GameObject targetToMoveTowards;
    [SerializeField] private LayerMask playerLayerMask;
    private Rigidbody rb;
    private bool MovingTowardsTarget = false;
    private float MovementSpeed;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        areaCollider = GetComponentInChildren<CheckAreaCollider>();
        EnemyStats = GetComponentInParent<EnemyStats>();
        MovementSpeed = EnemyStats.ReturnBaseMovementSpeed();
    }

    void Update()
    {
        targetToMoveTowards = areaCollider.ReturnTargetsInArea();

        if (targetToMoveTowards != null)
        {
            Vector3 targetPosition = targetToMoveTowards.transform.position;
            targetPosition.y = rb.transform.position.y;
            rb.transform.LookAt(targetPosition);

            // Calculate the movement direction and then move towards the target
            Vector3 moveDirection = (targetPosition - rb.transform.position).normalized;
            rb.velocity = moveDirection * MovementSpeed;
            Debug.DrawLine(rb.transform.position, targetPosition, Color.red);
        } else
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 0.05f * Time.deltaTime);
            checkMovingTowardsTarget();
    }

    void checkMovingTowardsTarget(){
            if (rb.velocity == Vector3.zero){
                MovingTowardsTarget = false;
            } else {
                MovingTowardsTarget = true;
            }
    }


    public bool returnMoveState(){
        return MovingTowardsTarget;
    }
}
