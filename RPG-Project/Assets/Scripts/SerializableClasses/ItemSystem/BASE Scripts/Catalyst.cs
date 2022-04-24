using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "BlankWeapon", menuName = "Weapon")]
public class Catalyst : Item
{
    public int Damage; //more a multiplier than actual damage for the catalyst, like dark souls maybe (magic augmentation stats)
    // Make a magical dmg
    public DamageType Damagetype;
    public enum DamageType
    {
        ELEM,
        LIGHT,
        DARK,

    }

    public override bool EquipItem()
    {
    
        if (base.EquipItem() == true)
        {
            if (PlayerData.EquipedCatalyst != null)
            {
                PlayerData.AddItem(PlayerData.EquipedCatalyst);
                PlayerData.EquipedCatalyst = this;
                PlayerData.RemoveItem(this);
                return true;
            }
            else
            {
                PlayerData.EquipedCatalyst = this;
                PlayerData.RemoveItem(this);
                return true;
            }



        }
        else
        {
            PlayerData.RemoveItem(this);
            PlayerData.AddItem(PlayerData.EquipedCatalyst);
            PlayerData.EquipedCatalyst = this;
            return true;
        }

    }

    public override bool UnequipItem()
    {
        
        if (PlayerData.CheckInventorySpace() == true)
        {
            PlayerData.AddItem(this);
            PlayerData.EquipedCatalyst = null;
            return true;

        }
        else
        {
            FindObjectOfType<UIMaster>().UIWarning(" My Backpack is full...");
            return false;
        }
    }

}
