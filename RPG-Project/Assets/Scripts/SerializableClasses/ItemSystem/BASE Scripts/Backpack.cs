using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "BlankBackpack", menuName ="Backpack")]
public class Backpack : Item
{
    public int Size;


    public override bool EquipItem()
    {

        //Cant just use the armour code, as backpack needs to check if after unequiped, there is still itens on null slots;
        //Check if the space is bigger than the last bag, if it is, just add more space and its ok
        //if not, check everything, remember that the inventory and the ui are separated, so we need to work on it

        if(PlayerData.EquipedBackpack == null && this.Size > 0)  // <-- Yes, it gives error if the backpacks are of negative space, but, cmom, its not like we are violating space-time rules (unless...)
        {
            
            PlayerData.RemoveItem(this);
            PlayerData.EquipedBackpack = this;
            PlayerData.UpdatePlayerStats();
            UIMaster.UpdateUIInv();
            return true;
        }
        else
        {
            if(PlayerData.GetInventory().Count <= (PlayerData.GetMinInventorySpace() + this.Size))
            {
                PlayerData.RemoveItem(this);
                PlayerData.AddItem(PlayerData.EquipedBackpack);
                PlayerData.EquipedBackpack = this;
                PlayerData.UpdatePlayerStats();
                UIMaster.UpdateUIInv();
                return true;

            }

            if (PlayerData.GetInventory().Count > PlayerData.GetMinInventorySpace() + this.Size) 
            {
                FindObjectOfType<UIMaster>().UIWarning("I Can't Fit All My Things In This Bag");
                return false;
            }

            return false;


        }

       
       
    }

    public override bool UnequipItem()
    {
        Debug.Log("Passed Unequip");
        if(PlayerData.GetInventory().Count >= PlayerData.GetMinInventorySpace())
        {
            FindObjectOfType<UIMaster>().UIWarning("Where Will I Put All Of My Stuff Then?");
            return false;
        }
        else
        {
            //check for space added including the bag
            PlayerData.EquipedBackpack = null;
            PlayerData.AddItem(this);
            PlayerData.UpdatePlayerStats();
            return true;
        }
        
        
    }
}
