using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using static Task;

//made the code a while ago, so its kinda shit, not refactoring it
public class UIMaster : MonoBehaviour
{
    private AudioHandler audioHandler;
    //MainUI
    [SerializeField] protected Image MapIcon;
    [SerializeField] protected Text PlayernameTextBox;
    [SerializeField] protected Text WarningPanelText;
    public static bool IsUIActive = true;
    [SerializeField] protected GameObject UISides;
    [SerializeField] protected GameObject WarningPanel;
    [SerializeField] protected GameObject UIUpdateIcon;


    private Animator UIIconAnimator;


    //InventoryUI
    [SerializeField] protected GameObject InventoryPanel;
    [SerializeField] protected GameObject ItemSlotMaster;

    [SerializeField] protected Text MCHealthComentary;
    [SerializeField] protected Text MCSanityComentary;

    [SerializeField] protected Sprite FemaleChar;
    [SerializeField] protected Sprite MaleChar;
    [SerializeField] protected Image HealthMonitor;

    private Animator HealthMonitorAnimator;

    [SerializeField] protected static ItemSlotScript[] ItemSlotArray;

    //CharUI
    [SerializeField] protected Image CharSprite;
    [SerializeField] protected GameObject EquipamentSlotMaster;

    [SerializeField] protected EquipamentSlot[] EquipamentSlotArray;
    [SerializeField] protected GameObject CharacterPanel;

    //StatsUI
    [SerializeField] protected Text PlayerNameStat;
    [SerializeField] protected Text PlayerAgeStat;
    [SerializeField] protected Text PlayerLevelStat;
    [SerializeField] protected Text PlayerCurrentXPStat;
    [SerializeField] protected Text NextLevelXPStat;
    [SerializeField] protected Text PlayerCurrentSpecializationStat;

    [SerializeField] protected Text EletronicsSkillLevelStat;
    [SerializeField] protected Text ChemistrySkillLevelStat;
    [SerializeField] protected Text FirstAidSkillLevelStat;
    [SerializeField] protected Text EnduranceSkillLevelStat;
    [SerializeField] protected Text HistorySkillLevelStat;
    [SerializeField] protected Text SpeechSkillLevelStat;

    [SerializeField] protected Text CurrentSkillPointsUI;

    [SerializeField] protected GameObject SkillButtonsPlus;
    [SerializeField] protected GameObject SkillButtonsMinus;
    [SerializeField] protected GameObject StatsPanel;

    //ConfirmBox
    [SerializeField] protected Text ConfirmationText;
    [SerializeField] protected Button YesButton;
    [SerializeField] protected Button NoButton;
    [SerializeField] protected Button IntBlocker;
    [SerializeField] protected GameObject ConfirmPanel;
    private bool ConfirmationVariable;




    //TaskUI
    [SerializeField] protected GameObject TaskLogPanel;


    void Awake()
    {
        PlayernameTextBox.text = PlayerData.Playername;
        ItemSlotArray = ItemSlotMaster.GetComponentsInChildren<ItemSlotScript>();
        EquipamentSlotArray = EquipamentSlotMaster.GetComponentsInChildren<EquipamentSlot>();
        HealthMonitorAnimator = HealthMonitor.GetComponent<Animator>();
        WarningPanel.SetActive(false);
        UpdateUIInv();
        UpdateSex();
        UpdateUIChar();
        UpdateUIStats();
        UISides.GetComponent<Animator>().SetBool("IsActive", IsUIActive);
        UIIconAnimator = UIUpdateIcon.GetComponent<Animator>();
        //These are constant, and will not change while the game is running.
        audioHandler = FindObjectOfType<AudioHandler>();
        PlayerNameStat.text = PlayerData.Playername;
        PlayerAgeStat.text = PlayerData.Age.ToString();

    }

    //Using a lambda function on a call for this couroutine, we can avoid writing a bunch of overloads
    public IEnumerator UIConfirm(System.Action callback, bool Forceclose = false)
    {
        
        ConfirmPanel.SetActive(true);

        
        var waitForButton = new WaitForUIButtons(YesButton, NoButton);
        yield return waitForButton.Reset();
   
        if (waitForButton.PressedButton == YesButton)
        {
            Debug.Log("Yes Pressed");
            ConfirmationVariable = true;
            callback();
            yield return ConfirmationVariable;
            ConfirmPanel.SetActive(false);
        }
        else
        {

            ConfirmationVariable = false;
            Debug.Log("No Pressed");
            yield return false;
            ConfirmPanel.SetActive(false);
        }


    }
    

    protected void AddSkillPointUI(int Skillcode)
    {
         StartCoroutine(UIConfirm(() => PlayerData.AddSkillPoint(Skillcode)));
        
    }

    public void UIWarning(string WarningText)
    {
        WarningPanelText.text = WarningText;
        WarningPanel.SetActive(true);
        StartCoroutine(HoldTrigger(2f,"Warning",WarningPanel));
        

    }

    public bool PanelCheck()
    {
        if (CharacterPanel.activeInHierarchy == false && InventoryPanel.activeInHierarchy == false && TaskLogPanel.activeInHierarchy == false && ConfirmPanel.activeInHierarchy == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //Toggle UI On or Off with animation, useful for dialogues. May throw some warnings on the console, but it won't matter that much.
    public void ToggleUI(bool IsActive)
    {
        IsUIActive = IsActive;
        if (IsUIActive == false)
        {
            CloseInventoryPanel();
            CloseCharacterPanel();
            CloseTaskLog();
            CloseStatsPanel();
            ForceClose();
            

        }
        UISides.GetComponent<Animator>().SetBool("IsActive", IsActive);
    }
    //Functions bellow, are to Close/Open the panels, I putted them in here because i may want to do some diferent things when the panels are opened.
    //Check delay for panels because of multiples clicks on UI cause bugs on animation
    //leave them public in case we want to open the panels via code like in a turorial
    public void OpenInventoryPanel()
    {
        
        if(InventoryPanel.activeInHierarchy == true) { return; }
       UpdateUIInv();
       CloseStatsPanel();
       CloseCharacterPanel();
       CloseTaskLog();
       InventoryPanel.SetActive(true);

    }

    public void OpenStatsPanel() 
    {
        if (StatsPanel.activeInHierarchy == true) { return; }
        CloseInventoryPanel();
        CloseCharacterPanel();
        CloseTaskLog();
        UpdateUIStats();
        Debug.Log(PlayerData.GetSkillPoints());
        StatsPanel.SetActive(true);
    }

    public void OpenTaskLog()
    {
        if (TaskLogPanel.activeInHierarchy == true) { return; }
        CloseCharacterPanel();
        CloseStatsPanel();
        CloseInventoryPanel();
        TaskLogPanel.SetActive(true);

    }

    public void OpenCharacterPanel()
    {
        if (CharacterPanel.activeInHierarchy == true) { return; }
        UpdateUIChar();
        CloseInventoryPanel();
        CloseStatsPanel();
        CloseTaskLog();
        CharacterPanel.SetActive(true);

    }

    //Do just one function with parameters for closing something CloseWindow(GameObject WindowToClose) - Oh yeah, i forgot why i did not do that, ffs, maybe we need to mess around with it while closing panels;
    public void CloseTaskLog()
    {
        if (TaskLogPanel.activeInHierarchy == false) { return; }
        else {StartCoroutine(WindowSynced(TaskLogPanel, false)); }

    }
    public void CloseStatsPanel()
    {
        if (StatsPanel.activeInHierarchy == false) { return; }
        else { StartCoroutine(WindowSynced(StatsPanel, false)); }
    }
    public void CloseCharacterPanel()
    {
        if (CharacterPanel.activeInHierarchy == false) { return; }
        else { StartCoroutine(WindowSynced(CharacterPanel, false)); }
    }
    public void CloseInventoryPanel()
    {
        if (InventoryPanel.activeInHierarchy == false) { return; }
        else { StartCoroutine(WindowSynced(InventoryPanel, false)); }
    }

    public void ForceClose()
    {
        if(ConfirmPanel.activeInHierarchy == false) { return; }
        StopCoroutine(UIConfirm(() => Dummy(), Forceclose:true));
        StartCoroutine(HoldTrigger(0f, "Warning",ConfirmPanel));
    }
    private void Dummy()
    {
        return;
    }

//==================//

    //UpdateUIchar is under developement, follows the same principle as the Inventory, loop trough all slots, and fix the items of Playerdata into them.


    public void UpdateUIChar()
    {

        for (int i = 0; i < EquipamentSlotArray.Length; i++)
        {
            EquipamentSlotArray[i].EquipamentSlotUpdateEquipedItem();
        }

    }

    //Animation for notificatiom, may expand this function with parameters for different animations
    public void UpdateUIAnim()
    {
        if (UIUpdateIcon.activeInHierarchy == true)
        {
            return;
        }

        UIUpdateIcon.SetActive(true);

        audioHandler.Play("UIUpdate");
        StartCoroutine(HoldBool(4));

    }

    //Only used once, to set the right sprite for the right sex on the Char Screen
    private void UpdateSex()
    {
        if(PlayerData.Sex == "M")
        {
            CharSprite.sprite = MaleChar;
        }
        else
        {
            CharSprite.sprite = FemaleChar;
        }
    }
    //Loops through all the slots on an Array (MasterSlot) and fix in each one of them via a function in them (ItemslotScript), an equivalent item that is on the Inventory
    public static void UpdateUIInv()
    { 

       
        
        for (int i = 1; i < ItemSlotArray.Length + 1; i++)
        {
            if (i <= PlayerData.GetInventory().Count)
            {
                ItemSlotArray[i - 1].ItemSync(PlayerData.GetInventory()[i - 1]);
                continue;
            }
            else
            {
                ItemSlotArray[i - 1].EmptyItem();
                continue;
            }



        }
        for (int i = PlayerData.GetInventorySpace(); i < ItemSlotArray.Length; i++)
        {
            ItemSlotArray[i].NullItem();
            continue;

        }

        FindObjectOfType<UIMaster>().ToggleUI(UIMaster.IsUIActive);


    }

    public void UpdateUIStats() 
    {
        if(PlayerData.GetSkillPoints() == 0)
        {
            SkillButtonsPlus.SetActive(false);
        }
        else
        {
            SkillButtonsPlus.SetActive(true);
        }

        PlayerLevelStat.text = PlayerData.GetCurrentLevel().ToString();
        PlayerCurrentXPStat.text = ((int)PlayerData.GetXP()).ToString();
        //mess with XP to Next Level

        EletronicsSkillLevelStat.text = PlayerData.GetEletronicsLevel().ToString();
        ChemistrySkillLevelStat.text = PlayerData.GetChemistryLevel().ToString();
        FirstAidSkillLevelStat.text = PlayerData.GetFirstAidLevel().ToString();
        EnduranceSkillLevelStat.text = PlayerData.GetEnduranceLevel().ToString();
        HistorySkillLevelStat.text = PlayerData.GetHistoryLevel().ToString();
        SpeechSkillLevelStat.text = PlayerData.GetSpeechLevel().ToString();

        PlayerCurrentSpecializationStat.text = PlayerData.GetSpecialization().ToString();

        PlayerCurrentXPStat.text = ((int)PlayerData.GetXP()).ToString();
        NextLevelXPStat.text = ((int)PlayerData.GetXP()).ToString();

        CurrentSkillPointsUI.text = PlayerData.GetSkillPoints().ToString();



        
    }

    //Just some UI sound function
    protected void ButtonClick()
    {
        audioHandler.Play("Tick");
    }






    //Couroutines for animation and Holding.


    IEnumerator WindowSynced(GameObject Window, bool State)
    {
        if(State == true)
        {
            yield return new WaitForSeconds(1.2f);
            Window.SetActive(true);
            
        }
        else
        {   //Will throw warning if windows are already closed, but no problems
            Window.GetComponent<Animator>().SetTrigger("Close");
            yield return new WaitForSeconds(1.0f);
            Window.SetActive(false);
        }
        

        
    }

    public static IEnumerator HoldTrigger(float seconds,string trigger,GameObject gameObject) 
    {
        yield return new WaitForSeconds(seconds);
        gameObject.GetComponent<Animator>().SetTrigger(trigger);
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
  
  

    //FIX THIS, KINDA USELESS
    IEnumerator HoldBool(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        UIUpdateIcon.SetActive(false);
    }
   

    /// <summary>
    /// Temporary Code Bellow, Will Change For Switch
    /// </summary>

    public void UpdateHealth()
    {
        HealthMonitorAnimator.Rebind();
        if(PlayerData.CurrentHealth >= 75)
        {
           
            HealthMonitorAnimator.SetTrigger("Good");
            MCHealthComentary.text = "I'm Fine At The Moment";
            
        }
        if (PlayerData.CurrentHealth >= 50 && PlayerData.CurrentHealth < 75)
        {
            HealthMonitorAnimator.SetTrigger("SlightWound");
            MCHealthComentary.text = "Nothing Too Serious, I Can Keep Going";
        }
        if (PlayerData.CurrentHealth >= 25 && PlayerData.CurrentHealth < 50)
        {
            HealthMonitorAnimator.SetTrigger("Wounded");
            MCHealthComentary.text = "I Need A Doctor Before It Gets Too Bad";
        }
        if (PlayerData.CurrentHealth >= 0 && PlayerData.CurrentHealth < 25)
        {
            HealthMonitorAnimator.SetTrigger("BadlyWounded");
            MCHealthComentary.text = "I Won't Make It...";

        }
        
    }

    public void UpdateSanity()
    {
        if(PlayerData.CurrentSanity >= 75)
        {
            
            MCSanityComentary.text = "My Mind Is Crystal Clear";
        }
        if (PlayerData.CurrentSanity >= 50 && PlayerData.CurrentSanity < 75)
        {

            MCSanityComentary.text = "I'm Feeling A Slight Headache";
        }
        if (PlayerData.CurrentSanity >= 25 && PlayerData.CurrentSanity < 50)
        {

            MCSanityComentary.text = "I'm Feeling Dizzy...";
        }
        if (PlayerData.CurrentSanity >= 0 && PlayerData.CurrentSanity < 25)
        {

            MCSanityComentary.text = "I Need a Break, This Is Too Much For Me...";
        }
    }
}


