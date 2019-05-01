using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject target;
    public bool targetLocked;

    public GameObject turretBarrel;
    public GameObject ProjectileSpawn;
    public GameObject projectile;
    public float fireRate;
    private bool roundLoaded;

    private void Start()
    {
        roundLoaded = true;
        target = GameObject.Find("TurretTarget");
    }

    private void Update()
    {
        // fire and detect player
        if (targetLocked)
        {
            turretBarrel.transform.LookAt(target.transform);

            if (roundLoaded)
            {
                fire();
            }          
        }
    }

    void fire()
    {
        Transform Projectile = Instantiate(projectile.transform,ProjectileSpawn.transform.position, Quaternion.identity);
        Projectile.transform.rotation = ProjectileSpawn.transform.rotation;
        roundLoaded = false;
        StartCoroutine(projectileRate());
    }

    IEnumerator projectileRate()
    {
        yield return new WaitForSeconds(fireRate);
        roundLoaded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            targetLocked = true;
        }
    }
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") 
		{
			targetLocked = false;
		}
	}
}
