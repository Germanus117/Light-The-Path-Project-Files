using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour {

    public float Health = 3f;

    public AudioClip enemyHurt;
    public AudioSource audioSource;
    Vector3 knockback;
    public float knockbackValue = 10.0f;
    public GameObject player;
    public Animator anim;

    public Recover recover;

    bool hitOnce;

    public static float damageIncrease = 0f;

    enemyDetect movement;
    AlienEnemyAttack attack;

    NavMeshAgent enemyNavMesh;

    CapsuleCollider enemyCollider;

    

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyNavMesh = GetComponent<NavMeshAgent>();
        enemyCollider = GetComponent<CapsuleCollider>();
        attack = GetComponentInChildren<AlienEnemyAttack>();
        movement = GetComponent<enemyDetect>();
        audioSource = GetComponent<AudioSource>();
        //anim = GetComponent<Animator>();
    }

    private void Update() {
        if(recover.knockback) {
            transform.position = Vector3.Lerp(transform.position, transform.position + knockback, Time.deltaTime * 5f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "sword")
        {
            if(!hitOnce) {
                other.GetComponentInParent<AudioSource>().Play();
                Debug.Log("hit");
                TakeDamage(1f + damageIncrease);
                hitOnce = true;
            }
            //GetComponent<enemyDetect>().enabled = false;
        }
        
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "sword") {
            hitOnce = false;
        }
    }



    public void TakeDamage(float amount)
    {
        knockback = new Vector3(0, 0, knockbackValue);
        knockback = player.transform.TransformVector(knockback);
        
        if (Health > 0)
        {
            //transform.Translate(knockback * Time.deltaTime, Space.World);
            
            anim.SetTrigger("isHit");
            Health -= amount;
            
            
            audioSource.Play();
            
        }


        if (Health <= 0)
        {
            Debug.Log("Enemy is dead!!");
            CombatLock.selectedTarget = null;
            CombatLock.targetLocked = false;
            attack.enabled = false;
            movement.enabled = false;
            enemyNavMesh.enabled = false;
            enemyCollider.enabled = false;
            anim.SetTrigger("Dead");
        }

    }

    
}
