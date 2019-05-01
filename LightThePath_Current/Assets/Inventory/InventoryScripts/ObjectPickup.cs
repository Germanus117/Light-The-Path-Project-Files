using UnityEngine;
using System.Collections;

public class ObjectPickup : MonoBehaviour {

    public InventoryObject equipable;

    public int lightPathID;
    public LightPath lightPathScript;

    public GameObject interactDialog;


    //[SerializeField] List<GameObject> Equipables = new List<GameObject>();


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (InputManager.interact)
            {
                Debug.Log("Picking up " + equipable.name);

                bool wasPickedUp = Inventory.instance.Add(equipable, lightPathID);

                if (wasPickedUp)
                {
                    interactDialog.SetActive(false);

                    if (equipable.name == "LightOrb")
                    {
                        //DrawLightScript.enabled = true;
                        StartCoroutine(lightPathScript.TurnOn());
                        
                    }
                    gameObject.SetActive(false);
                }

            }
        }
    }
}
