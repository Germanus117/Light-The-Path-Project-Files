using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton

    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found!");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    [HideInInspector]public int lightPathID;

    public int space = 9;  // Amount of item spaces

    // Our current list of items in the inventory
    public List<InventoryObject> equipables = new List<InventoryObject>();

    // Add a new item if enough room
    public bool Add(InventoryObject equipable, int ID)
    {
        if (equipable.showInInventory)
        {
            if (equipables.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }
            lightPathID = ID;
            equipables.Add(equipable);

            if (onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
                
        }
        return true;
    }

    // Remove an item
    public void Remove(InventoryObject equipable)
    {
        equipables.Remove(equipable);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
            
    }

}

