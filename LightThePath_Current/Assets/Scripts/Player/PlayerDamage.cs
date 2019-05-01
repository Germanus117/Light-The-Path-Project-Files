using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{

    public Slider healthSlider;
    public int playerHealth = 30;
    public int maxPlayerHealth = 50;
    public AudioSource playerHurt;
	public static bool tookDamage = false;
	public float tookDamageInterval;
    public static int defensiveBoost = 0;

    public Transform spawnPoint;


    [HideInInspector] public Vector3 Respawnpos;

    public static Animator anim;

    public static bool shielding;


    private void Start()
    {
        Respawnpos = spawnPoint.transform.position;
        anim = GetComponent<Animator>();
        print(playerHealth);
        //playerHurt = GetComponent<AudioSource>();
        Respawnpos = transform.position;
    }
    private void Update()
    {
		if (tookDamage) {
			if (tookDamageInterval > 1) {
				tookDamage = false;
			}
			tookDamageInterval += Time.deltaTime;
		}
        healthSlider.value = playerHealth / (maxPlayerHealth * 1f);

        if (playerHealth < 0f)
        {
            playerHealth = 0;

        }
        if (playerHealth > maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }

        if (Input.GetMouseButton(1) || InputManager.block)
        {
            shielding = true;
        }
        else
        {
            shielding = false;
        }


    }
    public void TakeDamage(int amount)
    {
        playerHurt.Play();
        playerHealth -= amount / defensiveBoost;
		tookDamage = true;
        Debug.Log("OUCH!!!");
    }

    public void Death()
    {
		SceneManager.LoadScene("MainMenu");
    }

}
