using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPCMaster : MonoBehaviour
{
    //Masterclass that NPC's Master will derivate and override from.
    public Collider2D NPCColider;
    public Sprite NPCDefaultSprite;


    public void UpdateSprite(Sprite sprite)
    {
        //NPC.GetComponent<SpriteRenderer>().sprite = sprite;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    protected virtual void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject() == true)
        {
            return;
        }
        Debug.Log("Error, This Function Was Not Overwritten in A NPCMasterScript");
    }
    public virtual void Deactivate()
    {
        NPCColider.enabled = false;
        gameObject.GetComponent<Animator>().enabled = false;
        UpdateSprite(null);
    }


    public virtual void Activate(Sprite NPCDefaultSprite)
    {
        gameObject.SetActive(true);
        NPCColider.enabled = true;
        gameObject.GetComponent<Animator>().enabled = true;
        UpdateSprite(NPCDefaultSprite);
    }

    public IEnumerator DelayDeactivate(string Trigger,GameObject NPC,float Delay)
       {
        NPC.GetComponent<Animator>().SetTrigger(Trigger);
        yield return new WaitForSeconds(Delay);
        Deactivate();


       }


   












}
                            