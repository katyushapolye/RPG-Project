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
    public SpellCard InitialSpellcard2;
    public SpellCard InitialSpellcard3;
    public SpellCard InitialSpellcard4;

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


        PlayerData.AddItem(InitialWeapon);
        PlayerData.AddItem(InitialUpper);
        PlayerData.AddItem(InitialLower);
        PlayerData.AddItem(InitialShield);
        PlayerData.AddItem(InitialJunk);
        PlayerData.AddItem(InitialConsumable);
        PlayerData.AddItem(InitialBag2);
        PlayerData.GetGrimoire().Add(InitialSpellcard);
        PlayerData.GetGrimoire().Add(InitialSpellcard2);
        PlayerData.GetGrimoire().Add(InitialSpellcard3);
        PlayerData.GetGrimoire().Add(InitialSpellcard4);






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
            switch (s)
            {
                case "HealthLose":
                    PlayerData.CurrentHealth -= 55;
                    PlayerData.AddXP(225);
                    FindObjectOfType<UIMaster>().UpdateUIAnim();
                    break;

                case "UpdateUI":
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
                    BG.GetComponent<Animator>().SetTrigger("FadeIn");
                    break;
                case "FadeOut":
                    BG.GetComponent<Animator>().SetTrigger("FadeOut");

                    break;

                case "SurpriseKomachi":
                    Debug.Log("Komachi Surprised");
                    Komachi.GetComponent<Animator>().SetTrigger("KomachiSurprised");
                    break;

                case "FadeKomachiOut":
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
            //change to switch if gets bigger than 4 triggers
            
           
           
           
         


            
           


        }
        


    }
}

