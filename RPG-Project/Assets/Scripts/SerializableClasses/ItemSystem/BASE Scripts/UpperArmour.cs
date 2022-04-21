using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "BlankUpperArmour", menuName = "UpperAmour")]
public class UpperArmour : Item
{
    public int PhysicalDefense;
    public int MagicalDefense;

    public override bool EquipItem()
    {

            if (PlayerData.EquipedUpperArmour != null)
            {
                PlayerData.RemoveItem(this);
                PlayerData.AddItem(PlayerData.EquipedUpperArmour);
                PlayerData.EquipedUpperArmour = this;
                return true;
            }
            else
            {
                PlayerData.EquipedUpperArmour = this;
                PlayerData.RemoveItem(this);
                return true;
            }
   
    }

    public override bool UnequipItem()
    {

        if (PlayerData.CheckInventorySpace() == true)
        {
            PlayerData.AddItem(this);
            PlayerData.EquipedUpperArmour = null;
            return true;

        }
        else
        {
            FindObjectOfType<UIMaster>().UIWarning(" My Backpack is full...");
            return false;
        }
    }

}
