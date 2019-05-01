using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
//    public PlayerDamage playerHealth;
	public GameObject player;
    public float speed;
	private PlayerDamage playerHealth;
    

	void Start() {
		player = GameObject.Find ("Player_Character");
		playerHealth = player.GetComponent<PlayerDamage>();
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
            
            playerHealth.TakeDamage(10);   
			Destroy (gameObject);
        }
    }
    
}
