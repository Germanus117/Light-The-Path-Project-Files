using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{


    public GameObject enemy;
    //private bool startEnemiesHaveAppeared;
    private float spawnTime = 3.0f;
    public Transform[] spawnPoints;
    public GameObject[] fogWalls;
    public int numbOfEnemiesInArea;
    private int count = 0;
    public bool playerHasEntered;
    private bool scaleIncrease;
    GameObject[] remainingEnemies;
    //public GameObject spawnParticle;
	public bool battleHasHappened = false;
	public int checkNumbOfEnemies;
	public GameObject playerReward;
	public bool rewardOnce = true;
	public GameObject rewardPosition;

    void Start()
    {
        //startEnemiesHaveAppeared = false;
        scaleIncrease = false;
		battleHasHappened = false;
    }

    private void Update()
    {
		if (remainingEnemies != null) {
			checkNumbOfEnemies = remainingEnemies.Length;
		}
        remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (playerHasEntered)
        {
            if (spawnTime <= 0.0f)
            {
                spawnTime = Random.Range(2.0f, 3.5f);
            }
            else
            {
                spawnTime -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //int startEnemyIndex = spawnPoints.Length;
        if (other.gameObject.CompareTag("Player"))
        {
			if (!battleHasHappened) {
				playerHasEntered = true;
				for (int i = 0; i < fogWalls.Length; i++) {
					fogWalls [i].SetActive (true);
				}
			}
            if (!scaleIncrease)
            {
                Invoke("TerritorySize", 0);
            }
            if (count < numbOfEnemiesInArea && spawnTime <= 0.001f)
            {
                Invoke("Spawn", 0);
            }
			else if (count >= numbOfEnemiesInArea && remainingEnemies.Length < 1)
            {
				battleHasHappened = true;
				if (rewardOnce) {
					rewardOnce = false;
					Instantiate (playerReward, rewardPosition.transform);
				}
                for (int i = 0; i < fogWalls.Length; i++)
                {
                    fogWalls[i].SetActive(false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spawnTime = Random.Range(6.0f, 10.0f);
            count = 0;
            //startEnemiesHaveAppeared = false;
            playerHasEntered = false;
            CancelInvoke("Spawn");
            for (int i = 0; i < remainingEnemies.Length; ++i)
            {
                Destroy(remainingEnemies[i]);
            }
            if (scaleIncrease)
            {
                Invoke("TerritorySize", 0);
            }
        }
    }

    void Spawn()
    {
        //Debug.Log("enemy spawning...");
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        //Instantiate(spawnParticle, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        count++;
    }

    void TerritorySize()
    {
        if (playerHasEntered)
        {
            transform.localScale = new Vector3(3f, 1f, 3f);
            scaleIncrease = true;

        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            scaleIncrease = false;
        }
    }
}