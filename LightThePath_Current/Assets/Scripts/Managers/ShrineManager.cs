using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineManager : MonoBehaviour
{
    public int shrineID;
    public GameObject[] zones;
    public bool orbUsed;
    public GameObject darkness;
    //public LightActivation turnOn;
    public GameObject lightProbes;
    public GameObject InteractDialog;
    public GameObject shrineBeams;
    public InventoryUI inventoryScript;
    public GameObject inventoryUI;

    private void Update()
    {
        if (orbUsed)
        {
            //Add instanciation for the particle effect Stone made for putting orb in shrine
            for (int i = 0; i < zones.Length; i++)
            {
                zones[i].SetActive(true);
            }
            darkness.SetActive(false);
            lightProbes.SetActive(true);
            InteractDialog.SetActive(false);
            inventoryScript.inventoryIsActive = false;
            inventoryUI.SetActive(false);
            GameObject newBeam = Instantiate(shrineBeams);
            newBeam.transform.position = transform.position;
            newBeam.transform.rotation = transform.rotation;

            orbUsed = false;
        }
    }
}