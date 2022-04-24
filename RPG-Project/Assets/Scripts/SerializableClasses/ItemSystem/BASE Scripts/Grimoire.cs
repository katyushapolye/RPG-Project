using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "BlankGrimoire", menuName = "Grimoire")]
public class Grimoire : Item
{
    public int Size;

    public override bool EquipItem()
    {


            if (PlayerData.EquipedGrimoire != null)
            {
                PlayerData.RemoveItem(this);
                PlayerData.AddItem(PlayerData.EquipedGrimoire);
                PlayerData.EquipedGrimoire = this;
                PlayerData.SetMaxGrimoire(this.Size);
                return true;
            }
            else
            {
                PlayerData.EquipedGrimoire = this;
                PlayerData.SetMaxGrimoire(this.Size);
                PlayerData.RemoveItem(this);
                return true;
            }



     }
     

    

    public override bool UnequipItem()
    {

        if (PlayerData.CheckInventorySpace() == true)
        {
            PlayerData.AddItem(this);
            PlayerData.EquipedGrimoire = null;
            return true;

        }
        else
        {
            FindObjectOfType<UIMaster>().UIWarning(" My Backpack is full...");
            return false;
        }
    }

}
