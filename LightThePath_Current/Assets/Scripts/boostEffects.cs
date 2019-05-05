using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostEffects : MonoBehaviour
{
    public static bool boost;
	public static bool speed;
	public static bool damage;
	public static bool armor;
    public float boostTime;

    public bool timer;
    public float timeLeft;


    private void Start()
    {
		timeLeft = 0;
        timeLeft = boostTime;
        boost = false;
		speed = false;
		damage = false;
		armor = false;
    }

    public void Update()
    {
        if (timer)
        {

			if (timeLeft > 0) {
				timeLeft -= Time.deltaTime;
				boost = true;
				if (speed) {
					PlayerController.speedBoost = 10f;
				}
				if (damage) {
					EnemyHealth.damageIncrease = 5f;
				}
				if (armor) {
					PlayerDamage.defensiveBoost = 2;
				}
			}
            else
            {
				speed = false;
				damage = false;
				armor = false;
                boost = false;
                timer = false;
                timeLeft = boostTime;
                PlayerController.speedBoost = 0f;
                EnemyHealth.damageIncrease = 0f;
				PlayerDamage.defensiveBoost = 1;
            }
        }
    }
}
