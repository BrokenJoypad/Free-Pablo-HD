using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerOccluded : MonoBehaviour
{

    public bool PlayerOccluded = false;
    public GameObject Player;
    
    private Camera gameCamera;
    private Vector3 playerPosition;
    private GameObject hitObject;
    private Vector3 hitObjectPosition;
    

    void Start()
    {
        gameCamera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {    
        playerPosition = Player.transform.position;
        CheckOccluded();
    }

    void CheckOccluded()
    {
        Vector3 screenPlayerPosition = gameCamera.WorldToScreenPoint(playerPosition);
        Ray ray = gameCamera.ScreenPointToRay(screenPlayerPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.rigidbody != null)
            {
                hitObject = hit.rigidbody.gameObject;
                if (hitObject == Player)
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.green);
                    PlayerOccluded = false;
                }
                else
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.red);
                    PlayerOccluded = true;
                }
            }
        }
    }

}



