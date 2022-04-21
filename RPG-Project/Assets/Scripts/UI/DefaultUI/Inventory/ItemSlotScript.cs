using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotScript : MonoBehaviour
{
    public Item Item;
    public Image ItemIcon;
    public Sprite NullItemSprite;
    public Sprite NotEnoughSpaceSprite;
    public Text ItemName;
    public Text ItemType;
    public Text ItemDescription; //Might Change, Not good to have these...
    public Button RemoveButton;
    public Button EquipButton;
    public Button UseButton;
    public Button ItemInspectButton;


    //Functions For Updating UI//
    public void ItemSync(Item ItemToSync)
    {
        Item = ItemToSync;
        ItemaddUI();
    }
    public void ItemaddUI()
    {
        if (this.Item == null) 
        {
            return;
        }
        ItemIcon.sprite = Item.Sprite;
        ItemName.text = Item.Itemname;
        ItemType.text = Item.GetType().ToString();
        UseButton.interactable = true;
        RemoveButton.interactable = true;
        EquipButton.interactable = true;
        ItemInspectButton.interactable = true;
        ItemDescription.text = Item.ItemDescription;

    }
    public void EmptyItem()
    {
        ItemIcon.sprite = NullItemSprite;
        ItemName.text = "";
        ItemType.text = "";
        ItemDescription.text = "";
        UseButton.interactable = false;
        RemoveButton.interactable = false;
        EquipButton.interactable = false;
        ItemInspectButton.interactable = false;
        this.Item = null;
    }

    public void NullItem()
    {
        ItemIcon.sprite = NotEnoughSpaceSprite;
        ItemName.text = "";
        ItemType.text = "";
        ItemDescription.text = "";
        UseButton.interactable = false;
        RemoveButton.interactable = false;
        EquipButton.interactable = false;
        ItemInspectButton.interactable = false;


    }


    //Functions For Manipulating The Item Via Playerdata (Remove Item)


    private void ItemSlotRemove()
    {
        PlayerData.RemoveItem(Item); 
    }


    //Functions For Manipulating The Item Via Item-Based Functions (Equip Item, Use Item)//

    private void ItemSlotUseItem()
    {
        Debug.Log("UsedItem");
        //Play sound
        Item.UseItem();
          
        
    }

    private void ItemSlotEquipItem()
    {

        Item.EquipItem();
        FindObjectOfType<AudioHandler>().Play("Cloth");
        
    }


    private void ItemSlotInspectItem()
    {
        if (this.Item == null) { return; }
        Item.InspectItem();
    }

    //Functions to pass to the confirmbox

    //change the text before on the confirmation box
    //check if it is doable


    //Wrapper Functions, could optimize to call the item-functions from here

    public void ItemSlotEquipUI()
    {
        if(this.Item == null) { return;}
        if(Item.IsEquipable == false) { return; }
        StartCoroutine(FindObjectOfType<UIMaster>().UIConfirm(ItemSlotEquipItem));
    }

    public void ItemUseUI()
    {
        if (this.Item == null) { return; }
        if(this.Item.IsConsumable == true || this.Item.IsJunk)
        {
            StartCoroutine(FindObjectOfType<UIMaster>().UIConfirm(ItemSlotUseItem));
        }
    }

    public void ItemInspectUI()
    {
        //Dont need to confirm an inspection 
        ItemSlotInspectItem();
    }

    public void ItemRemoveUI()
    {
        if (this.Item == null) { return; }
        StartCoroutine(FindObjectOfType<UIMaster>().UIConfirm(ItemSlotRemove));
    }
}




