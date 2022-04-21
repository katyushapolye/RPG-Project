using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "BlankLowerArmour", menuName = "LowerAmour")]
public class LowerArmour : Item
{
    public int PhysicalDefense;
    public int MagicalDefense;

    public override bool EquipItem()
    {

     
            if (PlayerData.EquipedLowerArmour != null)
            {
                PlayerData.RemoveItem(this);
                PlayerData.AddItem(PlayerData.EquipedLowerArmour);
                PlayerData.EquipedLowerArmour = this;
                return true;
            }
            else
            {
                PlayerData.EquipedLowerArmour = this;
                PlayerData.RemoveItem(this);
                return true;
            }

    }

    public override bool UnequipItem()
    {

        if (PlayerData.CheckInventorySpace() == true)
        {
            PlayerData.AddItem(this);
            PlayerData.EquipedLowerArmour = null;
            return true;

        }
        else
        {
            FindObjectOfType<UIMaster>().UIWarning(" My Backpack is full...");
            return false;
        }
    }

}
