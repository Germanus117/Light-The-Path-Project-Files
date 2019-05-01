using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{



    public float stepSpeed;
    public float turnSmoothing;
    public int staminaUse;
    Animator anim;
    public static bool attacking;
    public AudioSource swordAudio;
    public AudioClip SwordHitSound;
    //public AudioSource attackAudio;
    //public AudioSource attack2Audio;
    public Collider[] swordColliders;

    int attackNumber;


    public static bool canTurn;
    public static float speed;
    public static float turnSmooth;



    bool combo;

    Stamina stamina;

    void Awake()
    {
        swordAudio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        stamina = GetComponent<Stamina>();

        attackNumber = 0;

        foreach (Collider colliders in swordColliders)
        {
            colliders.enabled = false;
        }

    }

    /*void Start()
    {
        anim = GetComponent<Animator>();
    }*/

    void Update()
    {
        if (InputManager.attack || Input.GetMouseButtonDown(0) && stamina.currentStamina != 0)
        {

            if (attackNumber == 0)
            {
                foreach (Collider colliders in swordColliders)
                {
                    colliders.enabled = true;
                }
                anim.SetBool("Attacking", true);
                if (!StaminaBoost.infStamina)
                {
                    stamina.currentStamina -= staminaUse;
                    stamina.timer = stamina.timeTillRegen;
                }

                attackNumber = 2;
                attacking = true;

            }
            else
            {
                Attack();
                if (!combo)
                {
                    combo = true;
                }
            }
        }

        if (PlayerDamage.shielding)
        {
            anim.SetBool("Attacking", false);
            attackNumber = 0;
            attacking = false;
            foreach (Collider colliders in swordColliders)
            {
                colliders.enabled = false;
            }
        }

    }

    public void Attack()
    {


        if (attackNumber == 1)
        {
            anim.SetTrigger("Attack_1");
            //attackAudio.Play();
            attackNumber++;
        }
        else if (attackNumber == 2)
        {
            anim.SetTrigger("Attack_2");
            //attackAudio.Play();
            //attack2Audio.Play();
            attackNumber--;
        }
        if (!StaminaBoost.infStamina)
        {
            stamina.currentStamina -= staminaUse;
            stamina.timer = stamina.timeTillRegen;
        }
            
    }

    public void Step()
    {
        speed = stepSpeed;
        canTurn = true;
        turnSmooth = 4f;
    }

    public void Halt()
    {
        speed = 0f;
        canTurn = false;
    }

    public void Turn()
    {
        turnSmooth = turnSmoothing;
    }

    public void AttackStart()
    {
        combo = false;
    }

    public void End()
    {
        if (InputManager.attack && stamina.currentStamina != 0)
        {
            
            combo = true;
        }

        if (!combo)
        {
            anim.SetBool("Attacking", false);
            attackNumber = 0;
            attacking = false;

            foreach (Collider colliders in swordColliders)
            {
                colliders.enabled = false;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            swordAudio.clip = SwordHitSound;
            swordAudio.Play();
        }
    }



}