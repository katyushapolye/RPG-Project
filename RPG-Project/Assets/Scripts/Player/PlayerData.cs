using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public static class PlayerData
{
    //Base Stats and Info, do gets and sets later
    public static string Playername = "Unknown Traveller"; //will set on char creationg later
    public static string Sex;
    public static int Age = 17; //set based on what year of school the player is


    public static int MaxHealth = 100;
    public static int MaxSanity = 100;
    //Current "hidden" stats
    public static int CurrentHealth;
    public static int CurrentSanity;
    //LevelStats the Level equation will be Lx = 100{(1.5)^[(x-1)]}

    private static int CurrentLevel = 1;
    private static float CurrentXP = 0;
    private static float NextLevelXP = 150;
    private static int CurrentSkillPoints = 0;

    private static int EletronicsLevel = 2;
    private static int ChemistryLevel = 2;
    private static int FirstAidLevel = 2;
    private static int EnduranceLevel = 2;
    private static int HistoryLevel = 2;
    private static int SpeechLevel = 2;


    //Specialization
    public enum Specialization
    {
        None,
        Melee, //change later, not fit for game - Overwhelming Firepower, Concentrated Firepower, Exaustive Firepower, Balanced;
        Ranged,
        Magic,
        


    }
    private static Specialization PlayerSpecialization = 0;



    //Inventory and Grimoire Var
    private static int CurrentInventorySpace = 6;
    private static int MinInventorySpace = 6;

    private static int maxPlayerGrimoire = 4;





    //We could save some logic using gets and sets, and maybe we should, the equiped weapon and shield is temporary, only magic combat, will substiture for grimoire and/or catalyst or other 
    public static Catalyst EquipedCatalyst;
    public static Grimoire EquipedGrimoire;
    public static UpperArmour EquipedUpperArmour;
    public static LowerArmour EquipedLowerArmour;
    public static Backpack EquipedBackpack;

    //Inventory and Grimoire

    private static List<Item> Inventory = new List<Item>();
    private static List<SpellCard> PlayerGrimoire = new List<SpellCard>();


    //Save Flags -> Only one for now

    static private bool combatFlag = false; //i'm changing

    static private List<QuestBase> PlayertaskLog =  new List<QuestBase>();
    static private QuestBase CurrentMainQuest;






    //Skill Code
    public static void AddSkillPoint(int SkillCode)
    {
        Debug.Log("Reached SkillFunc");
        Debug.Log("Skill Code: " + SkillCode.ToString());
  
            switch (SkillCode)
            {
                case 1:
                    EletronicsLevel += 1;
                    CurrentSkillPoints -= 1;
                    break;
                case 2:
                    ChemistryLevel += 1;
                    CurrentSkillPoints -= 1;
                    break;
                case 3:
                    FirstAidLevel += 1;
                    CurrentSkillPoints -= 1;
                    break;
                case 4:
                    EnduranceLevel += 1;
                    CurrentSkillPoints -= 1;
                    break;
                case 5:
                    HistoryLevel += 1;
                    CurrentSkillPoints -= 1;
                    break;
                case 6:
                    SpeechLevel += 1;
                    CurrentSkillPoints -= 1;
                    break;
                default:
                    return;
            }
        MonoBehaviour.FindObjectOfType<UIMaster>().UpdateUIStats();
        
     
    }

    //ALWAYS ADD EXP BY HERE, TO AVOID ERRORS
    public static void AddXP(float AmountOfExp)
    {
        CurrentXP += AmountOfExp;
        NextLevelXP = XPCalculation();
        if (CurrentXP >= NextLevelXP)
        {
            CurrentLevel += 1;
            CurrentXP = CurrentXP - NextLevelXP;
            CurrentSkillPoints += 1;
            NextLevelXP = XPCalculation();
            //userfeedback
            
        }
    }

    //Getters and setters

    public static float GetXP() { return CurrentXP; }
    public static int GetSkillPoints() { return CurrentSkillPoints; }
    public static int GetCurrentLevel() { return CurrentLevel; }
    public static ref List<Item> GetInventory() { return ref Inventory; }

    public static ref List<SpellCard> GetGrimoire() { return ref PlayerGrimoire; }
    public static Specialization GetSpecialization() { return  PlayerSpecialization; }
    
    public static int GetInventorySpace() { return CurrentInventorySpace;}

    public static int GetMinInventorySpace() { return MinInventorySpace; }

    public static int GetEletronicsLevel() { return EletronicsLevel; }
    public static int GetChemistryLevel() { return ChemistryLevel; }
    public static int GetFirstAidLevel() { return FirstAidLevel; }
    public static int GetEnduranceLevel() { return EnduranceLevel; }
    public static int GetSpeechLevel() { return  SpeechLevel; }
    public static int GetHistoryLevel() { return HistoryLevel; }

    public static bool GetCombatFlag() { return combatFlag; }

    //maybe we will ditch the idea of a global tasklog, maybe we can work with 2 secondary quests and always one main quest, the tasklog might be used to store past tasks.
    public static ref List<QuestBase> GetCompletedPlayerTaskLog() //needs to be a ref because we NEED to actually change the objects inside, i dont know when c# passes by value exactly
    {
        return ref PlayertaskLog;
    }

    public static ref QuestBase GetCurrentPlayerMainQuest() 
    { 
        if(CurrentMainQuest == null)
        {
            CurrentMainQuest = new QuestBase(); //garatee we do not pass null object, and will log meaning that something went wrongm the player always needs to have a main quest
            
        }
        return ref CurrentMainQuest; 
    }

    public static void SetCurrentPlayerMainQuest(QuestBase nextQuest)
    {
        if(CurrentMainQuest == null)
        {
            Debug.LogWarning("Player did not have main quest until now, Is this intended behaviour?");
            CurrentMainQuest = nextQuest;

        }
        else
        {
            PlayertaskLog.Add(CurrentMainQuest);
            CurrentMainQuest = nextQuest;
        }
    }






    public static void SetCombatFlag(bool state) { combatFlag = state; }

    public static void SetMaxGrimoire(int maxGrimoire) { maxPlayerGrimoire = maxGrimoire;}
   
    
    //Functions Here only make simple things such as removing and adding itens to the Inventory --> UI IS HANDLED ON ITEMSLOT and EQUIPAMENTSLOT
    // Every time there is a change in the inventory itself, the UI will be updated             --> ITEM-BASED INTERACTIONS ON THE ITEM ITSELF
    public static bool AddItem(Item Item)
    {
        if(Inventory.Count == CurrentInventorySpace)
        {
            MonoBehaviour.FindObjectOfType<UIMaster>().UIWarning("I Can't Fit Anything Else In My Bag!");
            return false;
        }
        if (Item == null) { return false; }
        else
        {
            Inventory.Add(Item);
            UIMaster.UpdateUIInv();
            //UICallback
            return true;
        }
    }

    public static void RemoveItem(Item Item)
    {
        if (Item == null) { return;}
        Inventory.Remove(Item);
        UIMaster.UpdateUIInv();
        
    }

    public static bool CheckInventorySpace()
    {
        if (Inventory.Count == CurrentInventorySpace)
        {
            return false;
        } 
        else
        {
            
            return true;
        }
    }

    public static void UpdatePlayerStats()
    {
        if(EquipedBackpack == null) { CurrentInventorySpace = 6; return;}
        else
        {
            CurrentInventorySpace = MinInventorySpace + EquipedBackpack.Size;
        }
   
        


    }

    public static int XPCalculation()
    {
       double NeededXP;
       NeededXP = 100 * (System.Math.Pow(1.5, (CurrentLevel)));
       return (int)NeededXP;
    }

    

}
