using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameCursor gameCursor;

    [Header("Variables")]
    [SerializeField] private bool canMove = false;
    [SerializeField] private float MoveSpeed = 0f;
    
    
    private float StoppingDistance = 0.5f;
    private Vector3 MoveToPosition;
    private Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        gameCursor = GameCursor.Instance;
    }

    void Update()
    {   
        checkCanMove();
        
        if (Input.GetButton("LClick"))
        {
            MoveToPosition = gameCursor.ReturnCursorPosition();
            rb.transform.LookAt(MoveToPosition);
            
        }




    }

    void FixedUpdate()
    {
        Vector3 distanceBetween = MoveToPosition - rb.position;
        
        if (canMove && distanceBetween.magnitude > StoppingDistance)
        {
            Vector3 movementDirection = distanceBetween.normalized;
            Vector3 movement = movementDirection * MoveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
        else
        {
            rb.velocity = Vector3.zero; // Stop the Rigidbody when close to the target
        }
    }

    void checkCanMove(){

        if (Physics.Raycast(transform.position + new Vector3(0, 1, 0), transform.forward, out RaycastHit hit, 1f)) {
            if (hit.rigidbody != null) {
                canMove = false;
            } else {
                canMove = true;
            }
        } else {
            canMove = true;
        }

    }


}
