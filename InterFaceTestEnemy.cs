using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterFaceTestEnemy : MonoBehaviour, IAttackable
{

    public string DamageMessage = "Ouch, I'm taking Damage";

    public void TakeDamage()
    {
        Debug.Log(DamageMessage);
    }
}
