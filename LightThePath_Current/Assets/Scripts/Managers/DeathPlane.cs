using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPlane : MonoBehaviour {

    public float timeTillRespawn;

    public static bool playerFell;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            playerFell = true;
            other.GetComponent<Animator>().SetTrigger("Fall_Death");
            StartCoroutine(Respawn());
        }
    }

    IEnumerator Respawn() {
        yield return new WaitForSeconds(timeTillRespawn);
        playerFell = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
