using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienEnemyAttack : MonoBehaviour {
    //private Animator animator;

    public bool inDistance;
    PlayerDamage playerHealth;
    public float timeBetweenAttacks = 3.0f;
    public float attackDistance;
    public GameObject player;
    public GameObject particle;
    public Transform particleSpawn;
    public float timer;
    public int attack = 3;
	public Animator anim;
	public int attackChoice = 0;
    bool canAttack;

    bool triggered;

    public AudioSource ShieldSound;
    public GameObject shield;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerDamage>();

        shield = GameObject.FindGameObjectWithTag("Shield");
        ShieldSound = shield.GetComponent<AudioSource>();
        //anim = GetComponent<Animator>();	
    }



    void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
        if(distanceToTarget < attackDistance) {
            inDistance = true;
        } else {
            inDistance = false;
        }

        if(inDistance) {
            Vector3 target = (player.transform.position - transform.position).normalized;

            if(Vector3.Dot(target, transform.forward) > 0f) {
                canAttack = true;
            } else {
                canAttack = false;
            }
        }


        timer += Time.deltaTime;


        if (timer >= timeBetweenAttacks && inDistance && canAttack && playerHealth.playerHealth > 0)
        {
			attackChoice = (Mathf.RoundToInt(Random.Range(1, 11)));
			if (attackChoice >= 1 && attackChoice < 4) {
				anim.SetTrigger ("enemy_attack");
				timer = 0f;
				attack = 3;
				timeBetweenAttacks = 2.5f;
				// Attack();
			} else if (attackChoice >= 4 && attackChoice < 5) {
				anim.SetTrigger ("enemy_quick_attack");
				timer = 0f;
				attack = 1;
				timeBetweenAttacks = 1.5f;
			} else if (attackChoice >= 5 && attackChoice < 7) {
				anim.SetTrigger ("enemy_heavy_attack");
				timer = 0f;
				attack = 6;
				timeBetweenAttacks = 3.5f;
			} else if (attackChoice >= 7) {
				timer = 0f;
				timeBetweenAttacks = 1.5f;
			}
		}
	}

    public void Attack()
    {
        timer = 0f;
        GameObject newParticle = Instantiate(particle);
        newParticle.transform.position = particleSpawn.position;
        float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);
        if(distanceToTarget < attackDistance) {
            if(PlayerDamage.shielding) {
                Vector3 target = (transform.position - player.transform.position).normalized;

                if(Vector3.Dot(target, player.transform.forward) < 0f) {
                    playerHealth.TakeDamage(attack);
                    if(playerHealth.playerHealth > 1) {

                        PlayerDamage.anim.SetTrigger("BackHit");
                        PlayerDamage.anim.SetBool("Attacking", false);
                        PlayerDamage.anim.SetFloat("WalktoRun", 0f);
                        PlayerDamage.anim.SetFloat("ShieldorMove", 0f);
                    } else {
                        PlayerDamage.anim.SetBool("Attacking", false);
                        PlayerDamage.anim.SetTrigger("Back_Death");
                        PlayerDamage.anim.SetFloat("WalktoRun", 0f);
                        PlayerDamage.anim.SetFloat("ShieldorMove", 0f);
                    }
                } else {
                    PlayerDamage.anim.SetTrigger("ShieldHit");
                    ShieldSound.Play();
                }
            } else {
                Vector3 target = (transform.position - player.transform.position).normalized;

                if(Vector3.Dot(target, player.transform.forward) < 0f) {
                    playerHealth.TakeDamage(attack);
                    if(playerHealth.playerHealth > 1) {
                        
                        PlayerDamage.anim.SetTrigger("BackHit");
                        PlayerDamage.anim.SetBool("Attacking", false);
                        PlayerDamage.anim.SetFloat("WalktoRun", 0f);
                        PlayerDamage.anim.SetFloat("ShieldorMove", 0f);
                    } else {
                        PlayerDamage.anim.SetBool("Attacking", false);
                        PlayerDamage.anim.SetTrigger("Back_Death");
                        PlayerDamage.anim.SetFloat("WalktoRun", 0f);
                        PlayerDamage.anim.SetFloat("ShieldorMove", 0f);
                    }
                } else {
                    playerHealth.TakeDamage(attack);
                    if(playerHealth.playerHealth > 1) {
                        
                        PlayerDamage.anim.SetTrigger("FrontHit");
                        PlayerDamage.anim.SetBool("Attacking", false);
                        PlayerDamage.anim.SetFloat("WalktoRun", 0f);
                        PlayerDamage.anim.SetFloat("ShieldorMove", 0f);
                    } else {
                        PlayerDamage.anim.SetTrigger("Front_Death");
                        PlayerDamage.anim.SetBool("Attacking", false);
                        PlayerDamage.anim.SetFloat("WalktoRun", 0f);
                        PlayerDamage.anim.SetFloat("ShieldorMove", 0f);
                    }
                }
            }
        }
        
    }

    // Update is called once per frame
    /*void Update ()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance < 2f)
        {
            if (!inDistance)
            {
                inDistance = true;
            }
            else
            {
                time -= Time.deltaTime;
            }
            if (time <= 0.0f)
            {
                animator.SetBool("Attack", true);
            }
        }
        else
        {
            animator.SetBool("Attack", false);
            inDistance = false;
            time = 2.0f;
        }
    }*/

}