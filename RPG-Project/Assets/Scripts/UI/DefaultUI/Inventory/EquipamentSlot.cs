using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipamentSlot : MonoBehaviour
{
    private Item EquipedItem;
    public Text ItemName;
    public Text ItemMainAttribute;
    public Text ItemMainAttributeValue;
    public CorrespondentItem Correspondent;
    public enum CorrespondentItem
    {
        Weapon,
        Shield,
        UpperArmour,
        LowerArmour,
        Backpack

    }



    public void EquipamentSlotUnequipItemUI() 
    {
        if (EquipedItem == null) { return; }
        this.EquipedItem.UnequipItem();
        EquipamentSlotUpdateEquipedItem();
    }

    public void EquipamentSlotUpdateEquipedItem()
    {
        //First Switch Will Check The Current Type Of Equipamentslot and set the EquipamentVariable accordingly
        switch (Correspondent)
        {
            case CorrespondentItem.Weapon:
                EquipedItem = PlayerData.EquipedWeapon;
                break;
            case CorrespondentItem.Shield:
                EquipedItem = PlayerData.EquipedShield;
                ItemMainAttribute.text = "PRT";
                break;
            case CorrespondentItem.UpperArmour:
                EquipedItem = PlayerData.EquipedUpperArmour;
                ItemMainAttribute.text = "DEF";
                break;
            case CorrespondentItem.LowerArmour:
                EquipedItem = PlayerData.EquipedLowerArmour;
                ItemMainAttribute.text = "DEF";
                break;
            case CorrespondentItem.Backpack:
                EquipedItem = PlayerData.EquipedBackpack;
                ItemMainAttribute.text = "CAP";
                break;
            default:
                this.EquipedItem = null;
                ItemMainAttribute.text = "";
                break;
        }

        //Second Switch will update the UI based on what type of Equipament Is on this EquipamentVariable, That was set on the first Switch
        //The Function on the switch is to make sure we dont try to use a Null on the comparison, as this causes a runtime error when trying to get values from it
        //I Created Local Variables to pass their values to the UI temporarely, they will just exist on the stack,
        //definetly need to revamp the UI, so use this just as some references

        switch (ReturnNonNullItemName(ref EquipedItem))
        {
            case "Weapon":
                Weapon weapon = (Weapon)EquipedItem;
                ItemName.text = weapon.Itemname;
                ItemMainAttribute.text = "DMG";
                ItemMainAttributeValue.text = weapon.Damage.ToString();
                break;
            case "Shield":
                Shield shield = (Shield)EquipedItem;
                ItemName.text = shield.Itemname;
                ItemMainAttribute.text = "PTC";
                ItemMainAttributeValue.text = shield.Protection.ToString();
                break;
            case "UpperArmour":
                UpperArmour upperarmour = (UpperArmour)EquipedItem; //Downgrade is lowkey kinda useful
                ItemName.text = upperarmour.Itemname;
                ItemMainAttribute.text = "DEF";
                ItemMainAttributeValue.text = ((upperarmour.PhysicalDefense + upperarmour.MagicalDefense) /2).ToString();

                break;
            case "LowerArmour":
                LowerArmour lowerarmour = (LowerArmour)EquipedItem;
                ItemName.text = lowerarmour.Itemname;
                ItemMainAttribute.text = "DEF";
                ItemMainAttributeValue.text = ((lowerarmour.PhysicalDefense + lowerarmour.MagicalDefense) / 2).ToString();

                break;
            case "Backpack":
                Backpack backpack = (Backpack)EquipedItem;
                ItemName.text = backpack.Itemname;
                ItemMainAttribute.text = "CAP";
                ItemMainAttributeValue.text = backpack.Size.ToString();

                break;
            default:
                ItemName.text = "";
                ItemMainAttribute.text = "";
                ItemMainAttributeValue.text = "";
                break;
        }
    }

    private string ReturnNonNullItemName(ref Item Item)
    {
        if (Item == null)
        {
            return "";
        }
        else
        {
            return Item.GetType().ToString();
        }
    }

    


    
}
