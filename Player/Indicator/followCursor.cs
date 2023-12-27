using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCursor : MonoBehaviour
{

    private GameCursor gameCursor;


    private Rigidbody rb;
    private Vector3 lookAtPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameCursor = GameCursor.Instance;
    }

    void Update()
    {
        lookAtPoint = new Vector3(gameCursor.ReturnCursorPosition().x, 0, gameCursor.ReturnCursorPosition().z);
        if((transform.position - lookAtPoint).magnitude > transform.localScale.x + 0.2f){
            rb.transform.LookAt(lookAtPoint);
        }
    }
}
