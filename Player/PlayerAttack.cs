using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private void OnCollisionEnter(Collision other) {
        Debug.Log("hit something");
    }
}
