using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBoost : MonoBehaviour {

    public static bool infStamina;
    public float infTime;

    public bool timer;
    private float timeLeft;


    private void Start()
    {
        timeLeft = infTime;
		infStamina = false;
    }

    public void Update()
    {
        if (timer)
        {
            
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                infStamina = true;
            }
            else
            {
                infStamina = false;
                timer = false;
                timeLeft = infTime;
            }
        }
    }

}
