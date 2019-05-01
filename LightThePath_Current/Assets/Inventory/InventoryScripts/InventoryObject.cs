using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "New Object", menuName = "Inventory/Equipable")]
[System.Serializable]
public class InventoryObject : ScriptableObject
{


    new public string name = "New Object";    // Name of the item
    public Sprite icon = null;				// Item icon

    public bool showInInventory = true;

    // Called when the item is pressed in the inventory
    public virtual void Use()
    {
        // Use the item
        // Something may happen

        Debug.Log("Using " + name);
    }

    // Call this method to remove the item from inventory
    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

    //Inventory.instance.Add(item);
}

