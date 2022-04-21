using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Blank", menuName = "Blank")]
public class Item : ScriptableObject
{
    public string Itemname;
    [TextArea(3,4)]
    public string ItemDescription;
    public bool IsEquipable;
    public bool IsConsumable;
    public bool IsJunk;
    public Sprite Sprite;





    //These Functions are called from the item-slot on Item-Based Interactions, such as Using, Equipping and Inspecting, Interactions such as Removing are perfomed on the Playerdata Functions
    public virtual void UseItem()
    {
        Debug.Log("This Item Did not had it's UseItem action overwritten");
        return;
    }
    public virtual bool EquipItem()
    {
        
        if(PlayerData.CheckInventorySpace() == false)
        {
            return false;
        }

        return true;
    }

    public virtual bool UnequipItem() { Debug.Log("Not Overwritten"); return false; }


    public void InspectItem()
    {
        Debug.Log(ItemDescription);
        Debug.Log(Itemname);
    }

 
}
