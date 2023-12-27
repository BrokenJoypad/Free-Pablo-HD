using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField, Range(0f, 500f)] private float BaseHealth;
    [SerializeField, Range(0f, 10f)] private float BaseMovementSpeed;
    [SerializeField, Range(0f, 500f)] private float BaseAttackDamage;
    [SerializeField, Range(0f, 100f)] private float BaseAttackRange;
    

    public float ReturnBaseHealth(){
        return BaseHealth;
    }

    public float ReturnBaseMovementSpeed(){
        return BaseMovementSpeed;
    }

    public float ReturnBaseAttackDamage(){
        return BaseAttackDamage;
    }

    public float ReturnBaseAttackRange(){
        return BaseAttackRange;
    }
}
