using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
//    public PlayerDamage playerHealth;
	public GameObject player;
    public float speed;
	private PlayerDamage playerHealth;
    private GameObject wall;
    

	void Start() {
		player = GameObject.Find ("Player_Character");
		playerHealth = player.GetComponent<PlayerDamage>();
        wall = GameObject.FindGameObjectWithTag("Wall");
	}

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        Destroy(gameObject, 10f);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
            playerHealth.TakeDamage(12);   
			Destroy (gameObject);
        }
        if(other.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
    
}
