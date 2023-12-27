using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckAreaCollider : MonoBehaviour
{

[SerializeField] private GameObject ObjectInArea;

private SphereCollider areaCollider;
private EnemyStats areaColliderRange;

void Start(){
    areaCollider = GetComponent<SphereCollider>();
    areaColliderRange = GetComponentInParent<EnemyStats>();
    areaCollider.radius =  areaColliderRange.ReturnBaseAttackRange();

}

    private void OnTriggerStay(Collider collision) {
        if(collision.gameObject != null){
            if (collision.gameObject.name == "Player"){
                ObjectInArea = collision.gameObject;
            } 
        } else{
                ObjectInArea = null;
            }
    }

    private void OnTriggerExit(Collider collision) {
        if(collision.gameObject == ObjectInArea){
            ObjectInArea = null;
        }
    }

    public GameObject ReturnTargetsInArea(){
        return ObjectInArea;
    }
}
