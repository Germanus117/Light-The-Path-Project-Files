using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEquipment", menuName = "Inventory/Equipment")]
public class Equipment : InventoryObject {

    public EquipmentSlot equipSlot;



    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        //use item
        //RemoveFromInventory();
    }

}

public enum EquipmentSlot { Health, Stamina, LightOrb, StrengthPotion, SpeedBoost, DefenseBoost }
