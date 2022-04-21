using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene4Manager : SceneMaster
{
    public DialogueBlock IntroDialogueBlock;
    public Item InitialWeapon;
    public Item InitialShield;
    public Item InitialUpper;
    public Item InitialLower;
    public Item InitialJunk;
    public Item InitialConsumable;
    public Item InitialBag;
    public Item InitialBag2;
    public SpellCard InitialSpellcard;
    
    public GameObject Komachi;

    public Sprite combatArenaMuenzuka;
    public Enemy dummyEnemy;
    

    private void Start()
    {
        if(PlayerData.GetCombatFlag() == true)
        {
            UIMasterVar.ToggleUI(true);
            Komachi.GetComponent<KomachiMaster>().KomachiLoad();
            
            return;
        }

        UIMasterVar.ToggleUI(true);


        PlayerData.AddItem(InitialWeapon);
        PlayerData.AddItem(InitialUpper);
        PlayerData.AddItem(InitialLower);
        PlayerData.AddItem(InitialShield);
        PlayerData.AddItem(InitialJunk);
        PlayerData.AddItem(InitialConsumable);
        PlayerData.AddItem(InitialBag2);
        PlayerData.GetGrimoire().Add(InitialSpellcard);
     


        PlayerData.EquipedBackpack = (Backpack)InitialBag;
        Komachi.GetComponent<KomachiMaster>().KomachiActivate();
        FindObjectOfType<DialogueHandler>().StartDialogue(IntroDialogueBlock);
        PlayerData.UpdatePlayerStats();
 

    }

    public override void UpdateSceneTrigger(string SceneTrigger)
    {
        string[] Triggers = SceneTrigger.Split(',') ;
        foreach(string s in Triggers)
        {
            //change to switch if gets bigger than 4 triggers
            if (SceneTrigger == "HealthLose")
            {
                //Do as such that, it exist a function that calls on playerdata for this kind of alteration;
                PlayerData.CurrentHealth -= 55;
                PlayerData.AddXP(225);
                FindObjectOfType<UIMaster>().UpdateUIAnim();
                continue;
            }
            if (SceneTrigger == "UpdateUI")
            {
                FindObjectOfType<UIMaster>().UpdateUIAnim();
                continue;
            }
            if (SceneTrigger == "FadeKomachiIn")
            {
                Debug.Log("Komachi FadeIn");
                Komachi.GetComponent<Animator>().SetTrigger("FadeIn");
                continue;

            }
            if (SceneTrigger == "DialogueCompleted")
            {
                Debug.Log("Dialogue Completed");
                PlayerData.SetCombatFlag(true);
                continue;

            }
            if (SceneTrigger == "CombatStart")
            {
                Debug.Log("CombatStart");
                CombatData.loadCombatData(combatArenaMuenzuka, dummyEnemy);
                TransitionHandler.LoadCombatLevel();



            }


        }
        


    }
}

