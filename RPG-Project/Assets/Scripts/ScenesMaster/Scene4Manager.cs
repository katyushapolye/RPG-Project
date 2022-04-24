using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene4Manager : SceneMaster
{
    public DialogueBlock IntroDialogueBlock;
    public Item InitialUpper;
    public Item InitialLower;
    public Item InitialJunk;
    public Item InitialConsumable;
    public Item InitialBag;
    public Item InitialGrimoire;
    public Item InitalCellphone;


    [SerializeField] protected GameObject Komachi;
    [SerializeField] protected GameObject BG;

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

        //This will be handled later correctly, debug only
        PlayerData.EquipedLowerArmour = (LowerArmour)InitialLower;
        PlayerData.EquipedUpperArmour = (UpperArmour)InitialUpper;
        PlayerData.AddItem(InitialJunk);
        PlayerData.AddItem(InitialConsumable);
        PlayerData.EquipedBackpack = (Backpack)InitialBag;
        PlayerData.AddItem(InitialGrimoire);
        PlayerData.AddItem(InitalCellphone);


        Komachi.GetComponent<KomachiMaster>().KomachiActivate();
        FindObjectOfType<DialogueHandler>().StartDialogue(IntroDialogueBlock);
        PlayerData.UpdatePlayerStats();

    }

    public void StartIntroCombatDialogue()
    {
        Debug.Log("DialogueConbat");
        UpdateSceneTrigger("CombatStart");
    }

    public override void UpdateSceneTrigger(string SceneTrigger)
    {
        string[] Triggers = SceneTrigger.Split(',') ;
        foreach(string s in Triggers)
        {
            switch (s)
            {
                case "HealthLose":
                    PlayerData.CurrentHealth -= 55;
                    PlayerData.AddXP(225);
                    FindObjectOfType<UIMaster>().UpdateUIAnim();
                    break;

                case "UpdateUI":
                    Debug.Log("UpdateUI");
                    FindObjectOfType<UIMaster>().UpdateUIAnim();
                    break;

                case "FadeKomachiIn":
                    Debug.Log("Komachi FadeIn");
                    Komachi.GetComponent<Animator>().SetTrigger("FadeIn");
                    break;
                case "DialogueCompleted":
                    Debug.Log("Dialogue Completed");
                    Komachi.GetComponent<NPCMaster>().Activate(Komachi.GetComponent<NPCMaster>().NPCDefaultSprite);
                    PlayerData.SetCombatFlag(true);
                    break;

                case "CombatStart":
                    Debug.Log("CombatStart");
                    CombatData.loadCombatData(combatArenaMuenzuka, dummyEnemy);
                    TransitionHandler.LoadCombatLevel();
                    break;

                case "FadeIn":
                    Debug.Log("FadeIn");
                    BG.GetComponent<Animator>().SetTrigger("FadeIn");
                    break;
                case "FadeOut":
                    Debug.Log("FadeOut");
                    BG.GetComponent<Animator>().SetTrigger("FadeOut");

                    break;

                case "SurpriseKomachi":
                    Debug.Log("Komachi Surprised");
                    Komachi.GetComponent<Animator>().SetTrigger("KomachiSurprised");
                    break;

                case "FadeKomachiOut":
                    Debug.Log("Komachi FadeOut");
                    Komachi.GetComponent<NPCMaster>().Deactivate();
                    break;


                default:
                    if(s == "")
                    {
                        break;
                    }
                    Debug.Log("Trigger Not Found!!");
                    break;
            }
            
           
           
           
         


            
           


        }
        


    }
}

