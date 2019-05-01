using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathParticle : MonoBehaviour {

    public float timeToParticleDeath;

	// Use this for initialization
	void Start () {
        StartCoroutine(DestroyParticle());
	}

    IEnumerator DestroyParticle() {
        yield return new WaitForSeconds(timeToParticleDeath);

        Destroy(this.gameObject);
    }
}
