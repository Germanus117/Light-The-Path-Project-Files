using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthPotion : MonoBehaviour
{   
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "StrengthPotion")
        {
            EnemyHealth.damageIncrease = 2f;
        }
           
    }  
}
