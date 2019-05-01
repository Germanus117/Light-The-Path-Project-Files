using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostEffects : MonoBehaviour
{
    public static bool boost;
    public float boostTime;

    public bool timer;
    private float timeLeft;


    private void Start()
    {
        timeLeft = boostTime;
        boost = false;
    }

    public void Update()
    {
        if (timer)
        {

            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                boost = true;
                PlayerController.speedBoost = 10f;
                EnemyHealth.damageIncrease = 5f;
                PlayerDamage.defensiveBoost = 2;
            }
            else
            {
                boost = false;
                timer = false;
                timeLeft = boostTime;
                PlayerController.speedBoost = 0f;
                EnemyHealth.damageIncrease = 0f;
            }
        }
    }
}
