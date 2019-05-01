using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {
    public GameObject deathParticle;

    GameObject parent;

    private void Start() {
        parent = gameObject.transform.parent.gameObject;
    }

    public void Death() {
        GameObject particle = Instantiate(deathParticle);
        particle.transform.position = parent.transform.position + Vector3.up * 1.5f;
        Destroy(parent);
    }
}
