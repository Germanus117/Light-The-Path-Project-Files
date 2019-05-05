using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


/* Sits on all InventorySlots. */

public class InventorySlot : MonoBehaviour
{

    public Image icon;
    public Button removeButton;

    public Slider healthSlider;
    public Slider staminaSlider;

    public Stamina currentStamina;
    public StaminaBoost StaminaBoost;

    public GameObject orbDialog;

    //public int newTimer;
    public GameObject[] shrines;
    public List<OrbDialogController> theShrines = new List<OrbDialogController>();
    /*public ShrineManager shrine1;
    public ShrineManager shrine2;
    public ShrineManager shrine3;
    public ShrineManager shrine4;
    public ShrineManager shrine5;*/
    public PlayerDamage playerHealth;
    public int amount = 30;

    public boostEffects boostEffects;

    public int lightPathID;


    public InventoryObject equipable;  // Current item in the slot

    public GameObject[] lightPath;
    public List<LightPath> lightPathScript = new List<LightPath>();

    public void Start()
    {
        lightPath = GameObject.FindGameObjectsWithTag("LightPath");
        foreach (GameObject light in lightPath)
        {
            lightPathScript.Add(light.GetComponent<LightPath>());
        }
        shrines = GameObject.FindGameObjectsWithTag("Shrine");
        foreach (GameObject shrine in shrines)
        {
            theShrines.Add(shrine.GetComponent<OrbDialogController>());
        }
    }

    // Add item to the slot
    public void AddItem(InventoryObject newItem, int ID)
    {
        equipable = newItem;
        lightPathID = ID;
        icon.sprite = equipable.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    // Clear the slot
    public void ClearSlot()
    {
        equipable = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    // If the remove button is pressed, this function will be called.
    public void RemoveItemFromInventory()
    {
        Inventory.instance.Remove(equipable);
    }

    // Use the item
    public void UseItem()
    {
        if (equipable != null)
        {
            equipable.Use();
            if (equipable.name == "Health")
            {
                playerHealth.playerHealth += amount;
                healthSlider.value = playerHealth.playerHealth;
                Debug.Log(name + " applying health");
                equipable.RemoveFromInventory();
            }
            else if (equipable.name == "Stamina")
            {

                StaminaBoost.timer = true;
                Debug.Log(name + " adding extra stamina");
                equipable.RemoveFromInventory();
            }
            else if (equipable.name == "LightOrb")
            {
                orbDialog.SetActive(false);

                foreach (OrbDialogController shrines in theShrines)
                {
                    if (shrines.inShrine == true)
                    {
                        foreach (LightPath light in lightPathScript)
                        {
                            if (light.LightPathID == lightPathID)
                            {
                                Debug.Log("Turnning off lights...");
                                StartCoroutine(light.TurnOff());

                                equipable.RemoveFromInventory();
                                Debug.Log("Lights off removed from inventory");
                                break;
                            }


                        }
                    }
                }
                //StartCoroutine(lightPathScript.TurnOff());

            }
            else if (equipable.name == "StrengthPotion")
            {
                Debug.Log(name + "Very Strong!");
                //EnemyHealth.damageIncrease = 5f;
                boostEffects.timer = true;
                boostEffects.damage = true;
                equipable.RemoveFromInventory();
            }

            else if (equipable.name == "SpeedBoost")
            {
                Debug.Log(name + "Speedster!");
                //PlayerController.speedBoost = 10f;
                boostEffects.timer = true;
                boostEffects.speed = true;
                equipable.RemoveFromInventory();
            }

            else if (equipable.name == "DefenseBoost")
            {
                Debug.Log(name + "Tanky!");
                boostEffects.timer = true;
                boostEffects.armor = true;
                equipable.RemoveFromInventory();
            }

        }
    }

}


