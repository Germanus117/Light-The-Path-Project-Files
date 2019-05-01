using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMoveBackHit : MonoBehaviour {

   
    public Vector3 moveDirection;
    public float force = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            moveDirection = other.transform.position - transform.position;
            moveDirection = moveDirection.normalized;



            GetComponent<Rigidbody>().AddForce(moveDirection * force * 300f); /*Force that knock's enemy back*/
        }


    }
}

