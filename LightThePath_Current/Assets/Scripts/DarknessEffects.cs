using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessEffects : MonoBehaviour
{
    public PlayerController playerMovement;
    public PlayerDamage playerHealth;
    private bool playerIn;
    private float timer = 0.7f;
    public bool playerDying = false;
    public float timerSoPlayerCanDie;

    void Start()
    {
        playerDying = false;
        playerIn = false;
        timerSoPlayerCanDie = 0;
    }


    void Update()
    {
        if (playerDying && timerSoPlayerCanDie < 0.2f)
        {
            PlayerDamage.anim.SetTrigger("Back_Death");
        }
        if (playerHealth.playerHealth < 1)
        {
            timerSoPlayerCanDie += Time.deltaTime;
            playerDying = true;
        }
        if (playerIn)
        {
            if (timer <= 0)
            {
                playerHealth.TakeDamage(10);
                timer = 0.5f;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        else
        {
            timer = 0.7f;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIn = true;
            playerMovement.normalSpeed = 1.5f;
            playerMovement.sprintingSpeed = 3.5f;
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        playerIn = false;
        playerMovement.normalSpeed = 10f;
        playerMovement.sprintingSpeed = 15f;
    }

}
