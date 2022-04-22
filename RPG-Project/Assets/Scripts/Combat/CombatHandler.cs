using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CombatHandler : MonoBehaviour
{
    //Debug//
    public SpellCard InitialSpellcard;

    //Gneral Vars
    private AudioHandler audioHandler;
    //UI PART OF VARIABLES

    [SerializeField] private GameObject combatbackGround = null;
    [SerializeField] private GameObject combatEnemyGameObject= null;

    [SerializeField] private GameObject grimoireSlotsParent = null;

    [SerializeField] private Text PlayerHealthText = null;
    [SerializeField] private Text PlayerSanityText = null;

    [SerializeField] private Text EnemyHealthText = null;

    [SerializeField] private GameObject PlayerGrimoire = null;

    private GameObject[] grimoireSlotsArray = new GameObject[4];
    



    //COMBAT SYSTEM VARIABLES -> Separate script will take care of decisions this will only be a medium through there, the current turn and the UI

    
    private Enemy currentEnemy = null;
    private uint currentTurn = 1;

    // Start is called before the first frame update

    void Awake()
    { //Awake calls while the editor is still open so funny behaviour can happen, specially using scriptable objects
  
        audioHandler = FindObjectOfType<AudioHandler>();

        InitializeCombatData();
        InitializeGrimoire();
        UpdateUI();

        

    }

    //Initialization Functions
    private void InitializeCombatData()
    {
        if(CombatData.GetEnemy() == null || CombatData.GetCombatBackGround() == null)
        {
            return;
        }
        this.combatbackGround.GetComponent<SpriteRenderer>().sprite = CombatData.GetCombatBackGround();
        //We need to create a copy of the data enemy for this instance, otherwise when knocking down it's health we will be alternating the object's value at runtime 
        currentEnemy = (Enemy)CombatData.GetEnemy().CreateClone();


       

        this.combatEnemyGameObject.GetComponent<SpriteRenderer>().sprite = currentEnemy.getSprite();

        Debug.Log("Enemy Name: "+ currentEnemy.name);

  

    }

    private void InitializeGrimoire()
    {
        for (int i = 0; i < grimoireSlotsParent.transform.childCount; i++)
        {
            grimoireSlotsArray[i] = grimoireSlotsParent.transform.GetChild(i).gameObject;
           
            if(PlayerData.GetGrimoire() == null)
            {
                grimoireSlotsArray[i].GetComponent<SpellcardSlotScript>().SetSpellCard(null);
                continue;
            }
            
        }
        for (int k = 0; k < PlayerData.GetGrimoire().Count; k++)
        {
            grimoireSlotsArray[k].GetComponent<SpellcardSlotScript>().SetSpellCard(PlayerData.GetGrimoire()[k]);
        }
    }

    //Update Functions

    private void UpdateUI()
    {

        UpdateEnemyHealth();
        UpdatePlayerStats();

 


        


    }

    private void UpdateEnemyHealth()
    {
        if(currentEnemy == null)
        {
            EnemyHealthText.text = "No Enemy was found - Null - ";
            return;
        }
        float enemyRelativeHealth = (float)currentEnemy.getHealth() / (float)currentEnemy.getMaxHealth();
        Debug.Log("Enemy Relative Debug Health:" + enemyRelativeHealth);
        if (enemyRelativeHealth >= 0.75f)
        {
            EnemyHealthText.text = "It seems to be doing fine for now.";
        }
        else if (enemyRelativeHealth >= 0.50f && enemyRelativeHealth < 0.75f)
        {
            EnemyHealthText.text = "I did some damage to it, but it can still fight just fine.";
        }
        else if (enemyRelativeHealth >= 0.25f && enemyRelativeHealth < 0.50f)
        {

            EnemyHealthText.text = "It's hurt pretty badly, it won't last long now!";
        }
        else if (enemyRelativeHealth < 0.25f && enemyRelativeHealth > 0.00f)
        {

            EnemyHealthText.text = "One final blow should do the job!";

        }
        else if (enemyRelativeHealth <= 0.00f)
        {

            EnemyHealthText.text = "It's Dead... I Think...";

        }

    }
    
    private void UpdatePlayerStats()
    {
        //switch for percentages later
        if (PlayerData.CurrentHealth >= 75)
        {

        
            PlayerHealthText.text = "I'm Fine At The Moment";

        }
        else if (PlayerData.CurrentHealth >= 50 && PlayerData.CurrentHealth < 75)
        {

            PlayerHealthText.text = "Nothing Too Serious, I Can Keep Going";
        }
        else if (PlayerData.CurrentHealth >= 25 && PlayerData.CurrentHealth < 50)
        {

            PlayerHealthText.text = "I Need A Doctor Before It Gets Bad";
        }
        else if (PlayerData.CurrentHealth >= 0 && PlayerData.CurrentHealth < 25)
        {

            PlayerHealthText.text = "I Won't Make It...";

        }

        if (PlayerData.CurrentSanity >= 75)
        {

           PlayerSanityText.text = "My Mind Is Crystal Clear";
        }
        else if (PlayerData.CurrentSanity >= 50 && PlayerData.CurrentSanity < 75)
        {

            PlayerSanityText.text = "I'm Feeling A Slight Headache";
        }
        else if (PlayerData.CurrentSanity >= 25 && PlayerData.CurrentSanity < 50)
        {

            PlayerSanityText.text = "I'm Feeling Dizzy...";
        }
        else if (PlayerData.CurrentSanity >= 0 && PlayerData.CurrentSanity < 25)
        {

            PlayerSanityText.text = "I Need a Break, RIGHT NOW!";
        }

    }


    //Wrapper UI functions The attack button on the grimoire is treated at each spell slot


    public void FleeButoonUI()
    {
        //run the check for sucess
    }

    public void InventoryButtonUI()
    {
        //opens inventory and enables usage of 1 item
    }

    public void OpenGrimoire()
    {
        PlayerGrimoire.SetActive(true);
        audioHandler.Play("OpenGrimoire");



    }
    public void CloseGrimoire()
    {
        StartCoroutine(WindowSynced(PlayerGrimoire, false,"CloseGrimoire",0.7f));
        audioHandler.Play("CloseGrimoire");
    }

    IEnumerator WindowSynced(GameObject Window, bool State,string triggerName,float delay)
    {
        if (State == true)
        {
            yield return new WaitForSeconds(delay);
            Window.SetActive(true);

        }
        else
        {   //Will throw warning if windows are already closed, but no problems
            Window.GetComponent<Animator>().SetTrigger(triggerName);
            yield return new WaitForSeconds(delay);
            Window.SetActive(false);
        }



    }



    //Combat Functions

    //Public wrapper for UI
    public void PlayerAttack(SpellCard spell)
    {
        PlayerAction(spell);
    }



   


    //private internal handling
    //The combat, although it apears turnbased, works more like a attack-counterattack type, the enemy takes action only after the player takes his and is alive,
    //and the player only takes an action after that if it is still alive after the counter attack; this is done because of how unity treats scritps in its
    //inerworkings, we cant have a loop that awaits for a click in the ui(grimoire) to register the action, it would be a lot more code for the buttonclickawait,
    //so we will just do it event.oriented

    
  
    private void PlayerAction(SpellCard spellcard) //Triggered by chosing a spell on the grimoire, flee and analyze are treated difentrly->maybe hidden spellcard?
    {
       
        //Player action

        Debug.Log("TURN: " + currentTurn);
        currentEnemy.setHealth(currentEnemy.getHealth() - spellcard.rawDamage);
        Debug.Log("ENEMY HEALTH: " + currentEnemy.getHealth());
        UpdateUI();
        //Animations
        //Check for enemy Death -> Stop the combat
        CloseGrimoire();
        //Enemy Attack "Animation"
        //enemy action
        //Check for player death -> stop the combat
       
        currentTurn++;
        //if not return to "initial state" 
       

        

        Debug.Log("Used: " + spellcard.name);
    }
    private void EnemyAction()
    {


    }

    private bool IsPlayerIncapacitaded()
    {
        if(PlayerData.CurrentHealth<=0 || PlayerData.CurrentSanity <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }




}
