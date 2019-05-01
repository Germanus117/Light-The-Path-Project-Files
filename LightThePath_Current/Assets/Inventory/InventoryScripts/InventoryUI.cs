using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public GameObject inventoryUI;

    public AudioSource StepSound;

    Inventory inventory;

    public InventorySlot[] slots;

    public GameObject player;
    public GameObject playerCam;

    public bool inventoryIsActive;

    // Use this for initialization
    void Start () {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        inventoryIsActive = false;

        player = GameObject.FindGameObjectWithTag("Player");
        playerCam = GameObject.FindGameObjectWithTag("CamScript");


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && inventoryIsActive == true)
        {
            if (inventoryIsActive == true)
            {
                inventoryUI.SetActive(false);
                inventoryIsActive = false;
            }
        }
        else if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(true);
            inventoryIsActive = true;
            Cursor.lockState = CursorLockMode.None;
            //Cursor.lockState = CursorLockMode.Confined;
        }
        if (!PauseMenu.GameIsPaused)
        {
            if (!IntroController.reading)
            {

                if (inventoryIsActive == true)
                {
                    player.GetComponent<PlayerController>().enabled = false;
                    player.GetComponentInChildren<Weapon>().enabled = false;
                    playerCam.GetComponent<CameraRig>().enabled = false;
                    StepSound.enabled = false;
                    Time.timeScale = 0f;
                    Cursor.visible = true;
                }
                else if (!inventoryIsActive)
                {
                    Time.timeScale = 1f;
                    Cursor.visible = false;
                    player.GetComponent<PlayerController>().enabled = true;
                    player.GetComponentInChildren<Weapon>().enabled = true;
                    playerCam.GetComponent<CameraRig>().enabled = true;
                }
            }
        }
    }

    void UpdateUI()
    {
         for (int i =0; i < slots.Length; i++)
        {
            if (i < inventory.equipables.Count)
            {
                slots[i].AddItem(inventory.equipables[i], inventory.lightPathID);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }

        Debug.Log(" Updating UI ");
    }
}
