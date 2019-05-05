using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour {

    public bool inTrigger;
    public GameObject interactDialog;
    public string dialogText;


    public bool isPickupObject;
    //public bool findObject;




    // Update is called once per frame
    void Update () {
        if (inTrigger)
        {
            if (InputManager.interact)
            {
                if (isPickupObject)
                {
                    interactDialog.SetActive(false);
                }

            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            inTrigger = true;
            interactDialog.SetActive(true);
            interactDialog.GetComponent<Text>().text = dialogText;

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            interactDialog.SetActive(false);
            inTrigger = false;
        }
    }
}
