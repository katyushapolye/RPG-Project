using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "BlankJunk", menuName = "Junk")]
public class Junk : Item
{
    public override bool EquipItem()
    {
        return false;
    }


    //Maybe a specific text for try to use diferent itens; (downgrade on slotscript, use an variable here)
    public override void UseItem()
    {
        Debug.Log("Item Junk");
        FindObjectOfType<UIMaster>().UIWarning("I Cannot Use This Item");
        return;
    }

    

}
