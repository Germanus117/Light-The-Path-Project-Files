using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrbDialogController : MonoBehaviour {


    public bool inTrigger;
    public GameObject orbDialog;
    public string dialogText;
    public string findObjectTxt;

    public bool isPickupObject;
    //public bool findObject;

    public Inventory inventory;
    public bool lightUsed;


    // Update is called once per frame
    void Update()
    {
        if (inTrigger)
        {
            if (InputManager.interact)
            {
                if (isPickupObject)
                {
                    orbDialog.SetActive(false);
                }

            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            if (!lightUsed)
            {
                if(inventory.equipables.Count == 0)
                {
                    orbDialog.SetActive(true);
                    orbDialog.GetComponent<Text>().text = findObjectTxt;
                }
                for (int i = 0; i < inventory.equipables.Count; i++)
                {
                    if (inventory.equipables[i] != null)
                    {

                        if (inventory.equipables[i].name == "LightOrb")
                        {
                            orbDialog.SetActive(true);
                            orbDialog.GetComponent<Text>().text = dialogText;
                            break;
                        }
                        else
                        {

                            orbDialog.SetActive(true);
                            orbDialog.GetComponent<Text>().text = findObjectTxt;
                        }
                    }
                    else
                    {
                        orbDialog.SetActive(true);
                        orbDialog.GetComponent<Text>().text = findObjectTxt;
                    }
                }
            }

        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            orbDialog.SetActive(false);
            inTrigger = false;
        }
    }
}
