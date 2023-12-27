using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    [SerializeField] bool isAttacking = false;
    [SerializeField] bool DoingDamage = false;

    [SerializeField] GameObject Weapon;

    private PlayerState playerState;
    private bool WeaponHit = false;


    void Start()
    {
        playerState = GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WeaponHit = Weapon.GetComponent<ReturnHitObject>().ReturnHitConfirmation();
        isAttacking = playerState.IsPlayerAttacking();


        if(isAttacking && WeaponHit){
            DoingDamage = true;
        } else{
            DoingDamage = false;
        }

    }
}
