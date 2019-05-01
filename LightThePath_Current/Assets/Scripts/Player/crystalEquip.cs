using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class crystalEquip : MonoBehaviour
{
    public GameObject crystalLight;
    public AudioSource crystalSound;


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if (InputManager.interact)
            {
                crystalSound.Play();
                gameObject.SetActive(false);
                crystalLight.SetActive(true);

            }
        }
    }



}

