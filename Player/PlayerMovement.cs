using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [Header("Variables")]
    [SerializeField] private bool canMove = false;
    [SerializeField] private float MoveSpeed = 0f;
    
    
    private float StoppingDistance = 0.5f;
    private Vector3 ClickedTargetPosition;
    private GameObject ClickedTargetGameObject;
    private Rigidbody rb;
    private GameCursor gameCursor;
    private bool ArrivedAtClickedTarget = false;

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

        if(ArrivedAtClickedTarget && ClickedTargetGameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            Attack();
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
        }
        else if(!canMove && distanceBetween.magnitude > StoppingDistance)
        {
            rb.velocity = Vector3.zero; // Stop the Rigidbody when close to the target
            ArrivedAtClickedTarget = false;
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
    }


}
