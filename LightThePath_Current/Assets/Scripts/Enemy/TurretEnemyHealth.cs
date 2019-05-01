using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurretEnemyHealth : MonoBehaviour
{

    public float Health = 3f;

    public AudioClip enemyHurt;
    //public AudioSource audioSource;
    Vector3 knockback;
    public GameObject player;
    public GameObject particleDeath;
    bool hitOnce;

    public static float damageIncrease = 0f;

    CapsuleCollider enemyCollider;



    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyCollider = GetComponent<CapsuleCollider>();
        //audioSource = GetComponent<AudioSource>();
        //anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "sword")
        {
            if (!hitOnce)
            {
                other.GetComponentInParent<AudioSource>().Play();
                Debug.Log("hit");
                TakeDamage(1f + damageIncrease);
                hitOnce = true;
            }
            //GetComponent<enemyDetect>().enabled = false;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "sword")
        {
            hitOnce = false;
        }
    }



    public void TakeDamage(float amount)
    {
        if (Health > 0)
        {
            //transform.Translate(knockback * Time.deltaTime, Space.World);
            Health -= amount;


            //audioSource.Play();

        }


        if (Health <= 0)
        {
            Debug.Log("Enemy is dead!!");
            CombatLock.selectedTarget = null;
            CombatLock.targetLocked = false;
            enemyCollider.enabled = false;
            Invoke("Death", 0);
        }

    }

    void Death()
    {
        Instantiate(particleDeath, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), Quaternion.identity);
        Destroy(this.gameObject);
    }
    
}