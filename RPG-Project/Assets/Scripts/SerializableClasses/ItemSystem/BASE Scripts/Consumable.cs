using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "BlankConsumable", menuName = "Consumable")]

public class Consumable : Item
{
    public int HealingValue;
    //Maybe some item modifiers later, based on a enumerator with varius diferent status.
    //not for now, too much shit to worry about

    

    public override bool EquipItem()
    {
        
        return false;

    }


    public override void UseItem()
    {
        if(PlayerData.CurrentHealth == PlayerData.MaxHealth)
        {
            FindObjectOfType<UIMaster>().UIWarning("I Can't Eat Anything Else, I'm Full");
            return;
        }
        else
        {
            PlayerData.CurrentHealth += this.HealingValue;
            if(PlayerData.CurrentHealth > PlayerData.MaxHealth) { PlayerData.CurrentHealth = PlayerData.MaxHealth; } //stops the overflow
            PlayerData.RemoveItem(this);
            UIMaster.UpdateUIInv();
            FindObjectOfType<UIMaster>().UpdateHealth();
            FindObjectOfType<UIMaster>().UpdateSanity();
            return;
        }

    
    }

   

}
