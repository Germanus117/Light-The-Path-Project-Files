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
    public ShrineManager lightsOn;
    public ShrineManager lightsOn1;
    public ShrineManager lightsOn2;
    public ShrineManager lightsOn3;
    public ShrineManager lightsOn4;
    private int orbCompleteCount = 4;
    public ShrineManager inShrine;
    public PlayerDamage playerHealth;    
    public int amount = 30;
    //public float boostTimer;
    //public bool boostActive;
    //private bool strength;
    //private bool speed;

    public boostEffects boostEffects; 

    public int lightPathID;


    public InventoryObject equipable;  // Current item in the slot

    public GameObject[] lightPath;
    public List<LightPath> lightPathScript = new List<LightPath>();

    //void Update()
    //{
    //    if(boostActive == true)
    //    {
    //        if (strength == true)
    //        {
    //            EnemyHealth.damageIncrease = 5f;
    //        }

    //        if (speed == true)
    //        {
    //            PlayerController.speedBoost = 10f;
    //        }
    //    }
    //    else
    //    {
            
    //    }
    //}

    //public IEnumerator BoostEffects()
    //{       
    //    boostActive = true;
    //    Debug.Log("TRIGGEREEDDDDDDD!!!");
    //    yield return new WaitForSeconds(boostTimer);      
    //    boostActive = false;
    //    speed = false;
    //    strength = false;
    //}

    private void Start()
    {
        lightPath = GameObject.FindGameObjectsWithTag("LightPath");
        foreach(GameObject light in lightPath)
        {
            lightPathScript.Add(light.GetComponent<LightPath>());
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
                switch (orbCompleteCount)
                {
                    case 4:
                        lightsOn.orbUsed = true;
                        orbCompleteCount--;
                        break;
                    case 3:
                        lightsOn1.orbUsed = true;
                        orbCompleteCount--;
                        break;
                    case 2:
                        lightsOn2.orbUsed = true;
                        orbCompleteCount--;
                        break;
                    case 1:
                        lightsOn3.orbUsed = true;
                        orbCompleteCount--;
                        break;
                    case 0:
                        lightsOn4.orbUsed = true;
                        break;
                    default:
                        break;
                }

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
                //StartCoroutine(lightPathScript.TurnOff());
                
            }
            else if (equipable.name == "StrengthPotion")
            {
                Debug.Log(name + "Very Strong!");
                //EnemyHealth.damageIncrease = 5f;
                boostEffects.timer = true;
                equipable.RemoveFromInventory();               
            }

            else if (equipable.name == "SpeedBoost")
            {
                Debug.Log(name + "Speedster!");
                //PlayerController.speedBoost = 10f;
                boostEffects.timer = true;                                                             
                equipable.RemoveFromInventory();                
            }

            else if (equipable.name == "DefenseBoost")
            {
                Debug.Log(name + "Tanky!");
                boostEffects.timer = true;
                equipable.RemoveFromInventory();
            }

        }
    }
    
}


