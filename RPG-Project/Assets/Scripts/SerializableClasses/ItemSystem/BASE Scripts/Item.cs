using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Blank", menuName = "Blank")]
public class Item : ScriptableObject
{
    [SerializeField] protected string Itemname;
    [TextArea(3,4)]
    [SerializeField] protected string ItemDescription;
    [SerializeField] protected bool IsEquipable;
    [SerializeField] protected bool IsConsumable;
    [SerializeField] protected bool IsJunk;
    [SerializeField] protected Sprite Sprite;




    //Getters and setters

    public Sprite getSprite() { return this.Sprite; }

    public string getName() { return this.Itemname; }

    public string getDescription() { return this.ItemDescription; }

    public bool isEquipable() { return this.IsEquipable; }

    public bool isConsumable() { return this.IsConsumable; }

    public bool isJunk() { return this.IsJunk; }
   


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
