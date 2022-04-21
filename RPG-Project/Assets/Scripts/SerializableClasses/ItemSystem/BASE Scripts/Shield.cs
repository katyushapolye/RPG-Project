using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "BlankShield", menuName = "Shield")]
public class Shield : Item
{
    public int Protection;

    public override bool EquipItem()
    {


            if (PlayerData.EquipedShield != null)
            {
                PlayerData.RemoveItem(this);
                PlayerData.AddItem(PlayerData.EquipedShield);
                PlayerData.EquipedShield = this;
                return true;
            }
            else
            {
                PlayerData.EquipedShield = this;
                PlayerData.RemoveItem(this);
                return true;
            }



     }
     

    

    public override bool UnequipItem()
    {

        if (PlayerData.CheckInventorySpace() == true)
        {
            PlayerData.AddItem(this);
            PlayerData.EquipedShield = null;
            return true;

        }
        else
        {
            FindObjectOfType<UIMaster>().UIWarning(" My Backpack is full...");
            return false;
        }
    }

}
