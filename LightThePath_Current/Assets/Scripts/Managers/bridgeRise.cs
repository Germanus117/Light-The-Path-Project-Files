using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;





public class bridgeRise : MonoBehaviour
{

    
    public float riseTime = 5;

    public Transform startpoint;
    public Transform endpoint;
    public GameObject bridgeObject;
    //public AudioSource bridgeSound;
    //public AudioSource leverSound;

    Animator anim;

    //OffMeshLink offMeshLink;

    bool inTrigger;
    bool raiseBridge;

    private void Start() {
        anim = GetComponent<Animator>();
        bridgeObject.transform.position = startpoint.position;
        //offMeshLink = GetComponentInChildren<OffMeshLink>();
    }

    private void Update() {
        if(inTrigger) {
            if(InputManager.interact) {
                raiseBridge = !raiseBridge;
                //leverSound.Play();
                //bridgeSound.Play();
                if(raiseBridge) {
                    anim.SetTrigger("Forward");
                } else {
                    anim.SetTrigger("Back");
                }
            }
        }

        if(raiseBridge) {
            bridgeObject.transform.position = Vector3.Lerp(bridgeObject.transform.position, endpoint.position, Time.deltaTime * riseTime);
            //offMeshLink.activated = true;
        } else {
            bridgeObject.transform.position = Vector3.Lerp(bridgeObject.transform.position, startpoint.position, Time.deltaTime * riseTime);
            //offMeshLink.activated = false;
        }
    }


    void OnTriggerEnter(Collider other)
    {
       if (other.tag == "Player")
        {
            inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player") {
            inTrigger = false;
        }
    }
}