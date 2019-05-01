using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

    public Transform player;

    public float detectionRadius;

    NavMeshAgent agent;

    NavMeshObstacle obstacle;

    bool wait;

    bool triggered;

    Animator anim;

    bool playerInRange;

	// Use this for initialization
	void Start () {
        anim = GetComponentInChildren<Animator>();

        agent = GetComponent<NavMeshAgent>();

        obstacle = GetComponent<NavMeshObstacle>();
	}
	
	// Update is called once per frame
	void Update () {

        PlayerCheck();
        if((player.position - transform.position).sqrMagnitude < Mathf.Pow(agent.stoppingDistance, 2)) {
            agent.enabled = false;

            obstacle.enabled = true;

            
            wait = true;
            triggered = false;

            Quaternion rotation = Quaternion.LookRotation(player.position - transform.position);

            rotation = Quaternion.Euler(transform.eulerAngles.x, rotation.eulerAngles.y, transform.eulerAngles.z);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f);

            anim.SetBool("Walking", false);
        } else {
            obstacle.enabled = false;

            if(wait && !triggered) {
                StartCoroutine(Pause());
                triggered = true;
            }

            if(agent.enabled) {
                if(playerInRange) {
                    anim.SetBool("Walking", true);
                } else {
                    anim.SetBool("Walking", false);
                }
                agent.destination = player.position;
            }
        }
	}

    IEnumerator Pause() {
        yield return new WaitForSeconds(0.1f);
        agent.enabled = true;
        wait = false;
    }

    void PlayerCheck() {
        Collider[] playerCollider = Physics.OverlapSphere(transform.position, detectionRadius);

        foreach(Collider player in playerCollider) {
            if(player.tag == "Player") {
                playerInRange = true;
                agent.speed = 3.5f;
            } else {
                playerInRange = false;
                agent.speed = 0f;
            }
        }

        
    }
}
