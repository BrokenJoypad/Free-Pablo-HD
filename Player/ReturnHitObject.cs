using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class ReturnHitObject : MonoBehaviour
{

    private GameObject hitGameObject;
    public bool HitAnotherGameObject = false;



    private void OnCollisionEnter(Collision collision) {

        if(collision.gameObject != null) {
            hitGameObject = collision.gameObject;
            Debug.Log("Sword Blade Hit: " + hitGameObject.name);
            HitAnotherGameObject = true;
        } else{
            hitGameObject = null;
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject == hitGameObject) {
            hitGameObject = null; 
            HitAnotherGameObject = false;
        }
    }  




    public GameObject ReturnHitGameObject(){
        return hitGameObject;
    }

    public bool ReturnHitConfirmation(){
        return HitAnotherGameObject;
    }
}
