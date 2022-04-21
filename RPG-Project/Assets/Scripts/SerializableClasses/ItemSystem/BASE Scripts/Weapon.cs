using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "BlankWeapon", menuName = "Weapon")]
public class Weapon : Item
{
    public int Damage;
    // Make a magical dmg
    public DamageType Damagetype;

    public enum DamageType
    {
        CUT,
        BLT,
        PRC,

    }

    public override bool EquipItem()
    {
    
        if (base.EquipItem() == true)
        {
            if (PlayerData.EquipedWeapon != null)
            {
                PlayerData.AddItem(PlayerData.EquipedWeapon);
                PlayerData.EquipedWeapon = this;
                PlayerData.RemoveItem(this);
                return true;
            }
            else
            {
                PlayerData.EquipedWeapon = this;
                PlayerData.RemoveItem(this);
                return true;
            }



        }
        else
        {
            PlayerData.RemoveItem(this);
            PlayerData.AddItem(PlayerData.EquipedWeapon);
            PlayerData.EquipedWeapon = this;
            return true;
        }

    }

    public override bool UnequipItem()
    {
        
        if (PlayerData.CheckInventorySpace() == true)
        {
            PlayerData.AddItem(this);
            PlayerData.EquipedWeapon = null;
            return true;

        }
        else
        {
            FindObjectOfType<UIMaster>().UIWarning(" My Backpack is full...");
            return false;
        }
    }

}
