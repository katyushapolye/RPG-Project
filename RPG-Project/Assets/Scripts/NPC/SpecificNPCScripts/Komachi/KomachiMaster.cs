using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KomachiMaster : NPCMaster
{
    public DialogueBlock KomachiDialogueIdle;
    public DialogueBlock KomachiCombatStart;
    private DialogueHandler dialogueHandler;

    private void Awake()
    {
        dialogueHandler = FindObjectOfType<DialogueHandler>();


    }

    public void KomachiActivate()
    {
        Activate(NPCDefaultSprite);
   
    }

    //Might Scrape later
    public void KomachiLoad()
    {
        Activate(NPCDefaultSprite);
        gameObject.GetComponent<SpriteRenderer>().sprite = NPCDefaultSprite;
        gameObject.GetComponent<Animator>().SetTrigger("FadeIn");
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 0.0f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;


    }
   
    protected override void OnMouseDown()
    {
        if(EventSystem.current.IsPointerOverGameObject() == true)
        {
            return;
        }

        else
        {
            if(PlayerData.GetCombatFlag() == true)
            {
                //start a combat
                dialogueHandler.StartDialogue(KomachiCombatStart);
            }
            else
            {

            }
           
            dialogueHandler.StartDialogue(KomachiDialogueIdle);
            

        }
        

    }

   




}
        