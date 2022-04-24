using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipamentSlot : MonoBehaviour
{
    private Item EquipedItem;
    [SerializeField] protected Text ItemName;
    [SerializeField] protected Text ItemMainAttribute;
    [SerializeField] protected Text ItemMainAttributeValue;
    [SerializeField] protected CorrespondentItem Correspondent;
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
                EquipedItem = PlayerData.EquipedCatalyst;
                break;
            case CorrespondentItem.Shield:
                EquipedItem = PlayerData.EquipedGrimoire;
                ItemMainAttribute.text = "SIZE";
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
            case "Catalyst":
                Catalyst weapon = (Catalyst)EquipedItem;
                ItemName.text = weapon.getName();
                ItemMainAttribute.text = "DMG";
                ItemMainAttributeValue.text = weapon.Damage.ToString();
                break;
            case "Grimoire":
                Grimoire shield = (Grimoire)EquipedItem;
                ItemName.text = shield.getName();
                ItemMainAttribute.text = "PAG";
                ItemMainAttributeValue.text = shield.Size.ToString();
                break;
            case "UpperArmour":
                UpperArmour upperarmour = (UpperArmour)EquipedItem; //Downgrade is lowkey kinda useful
                ItemName.text = upperarmour.getName();
                ItemMainAttribute.text = "DEF";
                ItemMainAttributeValue.text = ((upperarmour.PhysicalDefense + upperarmour.MagicalDefense) /2).ToString();

                break;
            case "LowerArmour":
                LowerArmour lowerarmour = (LowerArmour)EquipedItem;
                ItemName.text = lowerarmour.getName();
                ItemMainAttribute.text = "DEF";
                ItemMainAttributeValue.text = ((lowerarmour.PhysicalDefense + lowerarmour.MagicalDefense) / 2).ToString();

                break;
            case "Backpack":
                Backpack backpack = (Backpack)EquipedItem;
                ItemName.text = backpack.getName();
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
