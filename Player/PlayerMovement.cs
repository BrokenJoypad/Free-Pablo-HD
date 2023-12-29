using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [Header("Variables")]
    [SerializeField] private bool canMove = false;
    [SerializeField] private float MoveSpeed = 0f;

    [Header("Movement Statuses")]
    [SerializeField] private bool isAttackingTarget = false;
    [SerializeField] private bool ArrivedAtClickedTarget = false;



    private float StoppingDistance = 2f;
    private Vector3 ClickedTargetPosition;
    private GameObject ClickedTargetGameObject;
    private Rigidbody rb;
    private GameCursor gameCursor;
    

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        gameCursor = GameCursor.Instance;
    }

    void Update()
    {   
        CheckCanMove();
        
        if (Input.GetButton("LClick"))
        {
            ClickedTargetPosition = gameCursor.ReturnCursorPosition();
            ClickedTargetGameObject = gameCursor.GetHitGameObject();
            rb.transform.LookAt(ClickedTargetPosition);
        }
    }

    void FixedUpdate()
    {
        MoveToClickedTarget();
        if(ArrivedAtClickedTarget && ClickedTargetGameObject != null && ClickedTargetGameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            Attack();
            isAttackingTarget = true;
        }
        else
        {
            isAttackingTarget = false;
        }
    }


    void MoveToClickedTarget()
    {
        Vector3 distanceBetween = ClickedTargetPosition - rb.position;

        if (canMove && distanceBetween.magnitude > StoppingDistance)
        {
            Vector3 movementDirection = distanceBetween.normalized;
            Vector3 movement = movementDirection * MoveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
            ArrivedAtClickedTarget = false;

        } else if(distanceBetween.magnitude < StoppingDistance) {
            ArrivedAtClickedTarget = true;
            rb.velocity = Vector3.zero;
        }

    }

    void CheckCanMove(){
        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out RaycastHit hit, 1f)) {
            if (hit.rigidbody != null || hit.collider != null) {
                canMove = false;
            } else {
                canMove = true;
            }
        } else {
            canMove = true;
        }
    }

    void Attack()
    {
        Debug.Log("Im attacking broooo");
        isAttackingTarget = true;
    }

    public bool ReturnPlayerAttacking()
    {
        return isAttackingTarget;
    }


}
