using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

//this script makes the enemy move randomly around the map using different paths every few seconds.

// in the Vector3 getNewRandomPostion() method, change the range to the size of your map/level and 
//put a nav mesh agent on your player object. If there are obstacles in your scene, go to the navigation 
//tab and put the obstacles to non walkable and bake it

public class enemyDetect : MonoBehaviour
{
	public int enemyInPathRandomNum;
	public bool enemyInPath = false;
    public int movementNum = 2;
    public float movementTimer = 20;
    public Transform positionTarget1;
    public Transform positionTarget2;
	public Transform positionTarget3;
	public Transform positionTarget4;
    public GameObject tempTarget1;
    public GameObject tempTarget2;
	public GameObject tempTarget3;
	public GameObject tempTarget4;
    public bool currentlyAggressive = false;
    public float aggressionTimer = 0;


    public Transform player;
    public float chaseRange;
    public float startChaseRange;
    public float speed;
    public GameObject target;
    public float stoppingDist;
    public Animator anim;

    public Recover recover;

    public bool inChaseRange;

    public GameObject[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;


    //public GameObject enemy;
    //public float spawnTime = 5f;
    //public Transform[] spawnPoints;

    void Start()
    {
        points = GameObject.FindGameObjectsWithTag("Patrol");
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");
        player = target.transform;
		tempTarget1 = GameObject.Find ("TempTarget1");
		tempTarget2 = GameObject.Find ("TempTarget2");
		tempTarget3 = GameObject.Find ("TempTarget3");
		tempTarget4 = GameObject.Find ("TempTarget4");
        positionTarget1 = tempTarget1.transform;
        positionTarget2 = tempTarget2.transform;
		positionTarget3 = tempTarget3.transform;
		positionTarget4 = tempTarget4.transform;

        agent.autoBraking = false;
        //InvokeRepeating("Spawn", spawnTime, spawnTime);
        //GotoNextPoint();
    }

    /*
    void GotoNextPoint()
    {

        if (points.Length == 0)
            return;

        anim.SetBool("Walking", true);

        agent.destination = points[destPoint].transform.position;

        destPoint = (destPoint + 1) % points.Length;
    }
    */
    

    //void Spawn()
    //{

    //    int spawnPointIndex = Random.Range(0, spawnPoints.Length);

    //    Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    //}

    void Update()
    {
        if (movementTimer > 10)
        {
            movementNum = Mathf.RoundToInt(Random.Range(1, 5));
            movementTimer = 0;
        }
        if (currentlyAggressive)
        {
            aggressionTimer += Time.deltaTime;
        }
        if (aggressionTimer >= 5)
        {
            currentlyAggressive = false;
            aggressionTimer = 0;
			enemyInPath = false;
        }
        if (!currentlyAggressive)
        {
            movementTimer += Time.deltaTime;
        }
        //if (!agent.pathPending && agent.remainingDistance < 0.5f)
            //GotoNextPoint();

        //distance to player,chase if close enough
        float distanceToTarget = Vector3.Distance(transform.position, player.position);
        if (distanceToTarget < startChaseRange)
        {
            inChaseRange = true;

        }

        if (inChaseRange)
        {
            if (distanceToTarget < chaseRange)
            {

                if (!recover.recovering)
                {
                    if (distanceToTarget < stoppingDist)
                    {
                        Vector3 target = (player.position - transform.position).normalized;
                        Vector3 tempTarget1 = (positionTarget1.position - transform.position).normalized;
                        Vector3 tempTarget2 = (positionTarget2.position - transform.position).normalized;
						Vector3 tempTarget3 = (positionTarget3.position - transform.position).normalized;
						Vector3 tempTarget4 = (positionTarget4.position - transform.position).normalized;
                        if (Vector3.Dot(target, transform.forward) < 0f)
                        {
                            agent.speed = speed;
                            Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
                            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 1f);
                            anim.SetBool("Walking", true);
                        }
                        else
                        {
                            agent.speed = 0f;
                            //GotoNextPoint();
                            anim.SetBool("Walking", false);
                        }


                    }
                    else
                    {
                        agent.speed = speed;
                        anim.SetBool("Walking", true);
                    }
                }
                else
                {
                    agent.speed = 0f;
                    //GotoNextPoint();
                    anim.SetBool("Walking", false);
                }
				if (enemyInPath && currentlyAggressive) {
					
					enemyInPathRandomNum = Mathf.RoundToInt (Random.Range (1, 3));
					if (enemyInPathRandomNum == 1) {
						agent.destination = tempTarget3.transform.position;
					} else {
						agent.destination = tempTarget4.transform.position;
					}
				}
                else if (currentlyAggressive)
                {
                    agent.destination = target.transform.position;
                }
                else if (movementNum == 1)
                {
                    agent.destination = target.transform.position;
                }
                else if (movementNum == 2)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 1f);
                    agent.speed = 0;
                    anim.SetBool("Walking", false);
                    Invoke("aggression", 5);
                }
                else if (movementNum == 3)
                {
                    agent.destination = tempTarget1.transform.position;
                }
                else if (movementNum == 4)
                {
                    agent.destination = tempTarget2.transform.position;
                }

            }
            else
            {
                anim.SetBool("Walking", false);
                //gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 0f;
                inChaseRange = false;
                //GotoNextPoint();
            }
        }


    }



    void aggression()
    {
        Debug.Log("aggression");
        currentlyAggressive = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("This needed a rigid body!?");

        if (other.CompareTag("BehaviorSwitch"))
        {
            if (!currentlyAggressive)
            {
                movementNum = 2;
            }
        }
    }

	public void RaycastDetect() 
	{
		Debug.Log ("RaycastDetect function");
		enemyInPath = true;
	}

    //	void OnTriggerExit(Collider other) {
    //		Debug.Log ("OnTriggerExit");
    //		if (other.CompareTag ("BehaviorSwitch")) {
    //			if (currentlyAggressive) {
    //				movementNum = movementNum = Mathf.RoundToInt (Random.Range (1, 5));
    //			}
    //		}
    //	}


}
