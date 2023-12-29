using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class GameCursor : MonoBehaviour
{

    public static GameCursor Instance;

    [SerializeField] private Camera gameCamera;
    [SerializeField] private LayerMask raycastIgnoreMask;

    private Vector3 mousePosition;
    private Vector3 PhysicalMousePosition;

    private GameObject hitObject;

    void Awake() {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
        

    void Update(){

        mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.y);
        Ray ray = gameCamera.ScreenPointToRay(mousePosition); 
        RaycastHit hit;

        LayerMask invertedLayerMask = ~raycastIgnoreMask;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, invertedLayerMask)){
            if (hit.rigidbody != null){
                hitObject = hit.rigidbody.gameObject;
                PhysicalMousePosition = hit.point;
                Debug.DrawLine(ray.origin, PhysicalMousePosition, Color.green);
            }
            else if(hit.collider != null)
            {
                hitObject = hit.collider.gameObject;
                PhysicalMousePosition = hit.point;
                Debug.DrawLine(ray.origin, PhysicalMousePosition, Color.blue);
            }
        }
    }

    public Vector3 ReturnCursorPosition(){
        return PhysicalMousePosition;
    }

    public GameObject GetHitGameObject(){
        return hitObject;
    }
       
}