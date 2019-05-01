using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour {

    public GameObject hitParticle;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Enemy") {
            GameObject particle = Instantiate(hitParticle);
            particle.transform.position = transform.position;
        }
    }
}
