using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler: MonoBehaviour
{

    //UI VARIABLES SET EDITOR//
    public GameObject DialogueBox;
    public Text DialogueText;
    public Text DialogueName;
    public Button UpdateDialogueButton;
    public Button EndDialogueButton;
    public SceneMaster Scenemaster;
    


    //Private Control Variables//
    private bool IsDialogueActive { get; set; }
    private int DialogueStep;
    private List<Dialogue> CurrentDialogues;
    private NPCMaster MainActor;
    private NPCMaster SecondaryActor;
    private AudioHandler AudioHandler;
    [SerializeField] protected Button ContinueButton;

    //Unique Temporary Variables//
    public DialogueBlock CurrentDialogueBlock;


    public void StartDialogue( in DialogueBlock dialogueBlock)
        /*Passing dialogueBlock by Reference to save memory, as these objects can be very large  (in = read-only),                                                  
         * Its not extremely useful, because it is safe code and as such, i cannot use pointers to point to its atributes. 
         * I Could use the reference from it to get all the values from, but it would get confusing pretty fast, so
         * I'll just let the memory manager handle it by declaring variables from this reference, 
         * I just wanted to avoid making 3 copies on the stack of the same object*/
    {
        //Fail-safe to stop duplicated initialization of dialogue//
        if(IsDialogueActive == true)
        {
            return;
        }


        //Setting Up The Current Dialogue Variables//
        CurrentDialogueBlock = dialogueBlock;
        IsDialogueActive = true;
        DialogueStep = 1;

        MainActor = dialogueBlock.MainActor.GetComponent<NPCMaster>();
        if (CurrentDialogueBlock.SecondaryActor != null) 
        {
            SecondaryActor = dialogueBlock.SecondaryActor.GetComponent<NPCMaster>();
        }
        CurrentDialogues = dialogueBlock.Dialogues;

        AudioHandler = FindObjectOfType<AudioHandler>(); //Put This in Awake() or Start();


        //Starting The First Line Of Dialogue//
        FindObjectOfType<UIMaster>().ToggleUI(false);
        DialogueBox.SetActive(true);
        MainActor.UpdateSprite(CurrentDialogues[0].MainActorSprite);
        if (SecondaryActor != null) { SecondaryActor.UpdateSprite(CurrentDialogues[0].SecondaryActorSprite); }
        DialogueName.text = FormatString(CurrentDialogues[0].SpeakerName,null);
        Scenemaster.UpdateSceneTrigger(CurrentDialogues[0].SceneTrigger);
        StartCoroutine(LetterAnimation(FormatString(CurrentDialogues[0].DialogueText,PlayerData.Playername),DialogueText, CurrentDialogues[DialogueStep-1].letterSpeed));
        AudioHandler.Play(CurrentDialogues[0].DialogueSoundName);
        UpdateDialogueButton.gameObject.SetActive(true);

        //Check if the dialogue is only one line, then, proceed to end it if it is True via the EndDialogue Method on the next player mousedown//:
        if (dialogueBlock.Dialogues.Count == 1)
        {
            EndDialogueButton.gameObject.SetActive(true);
            UpdateDialogueButton.gameObject.SetActive(false);
            return;
        }
        


    }


    public void UpdateDialogue()
    {
        //In Case the player Skips the dialogue before it ends its sequence, try to stop the past audio and the letter animations//
        StopAllCoroutines();
        try
        {
            AudioHandler.Stop(CurrentDialogues[DialogueStep - 1].DialogueSoundName);
        }
        catch (System.Exception)
        {
            Debug.Log("Audio To Stop Was Not Found");
        }
        

        //Update all the variables and displays//
        MainActor.UpdateSprite(CurrentDialogues[DialogueStep].MainActorSprite);
        if (SecondaryActor != null) { SecondaryActor.UpdateSprite(CurrentDialogues[DialogueStep].SecondaryActorSprite); }
        AudioHandler.Play(CurrentDialogues[DialogueStep].DialogueSoundName);
        StartCoroutine(LetterAnimation(FormatString(CurrentDialogues[DialogueStep].DialogueText, PlayerData.Playername),DialogueText,CurrentDialogues[DialogueStep].letterSpeed));
        Scenemaster.UpdateSceneTrigger(CurrentDialogues[DialogueStep].SceneTrigger);
        DialogueName.text = FormatString(CurrentDialogues[DialogueStep].SpeakerName, null);
        DialogueStep++;

        //Check to see If it is the last line//
        if (DialogueStep == CurrentDialogues.Count)
        {
            UpdateDialogueButton.gameObject.SetActive(false);
            EndDialogueButton.gameObject.SetActive(true);
            return;
        }

    }


    public void EndDialogue()
    {
        //Setting Up NPC's To Default State//
        MainActor.UpdateSprite(MainActor.NPCDefaultSprite);
        if (SecondaryActor != null) { SecondaryActor.UpdateSprite(SecondaryActor.NPCDefaultSprite); }
        //Setting UI Vars To False//
        EndDialogueButton.gameObject.SetActive(false);
        UpdateDialogueButton.gameObject.SetActive(false);
        FindObjectOfType<UIMaster>().ToggleUI(true);
        //Setting Current Dialogue Vars to Null/Default// -> Late setting the block because of animations 
        CurrentDialogues = null;
        MainActor = null;
        SecondaryActor = null;
        DialogueStep = 0;
        //Close the Dialogue Window//
        StartCoroutine(CloseWindowSynced(DialogueBox));
        Scenemaster.UpdateSceneTrigger(CurrentDialogueBlock.EndDialogueTrigger);
        CurrentDialogueBlock = null;

    }








    IEnumerator CloseWindowSynced(GameObject Window)
    {
        Window.GetComponent<Animator>().SetTrigger("Close");
        yield return new WaitForSeconds(1.0f);
        Window.SetActive(false);
        IsDialogueActive = false;
        StopAllCoroutines();

    }


    //despite my best efforts, because of the letter animation i couldn't do a italic option..
    private string FormatString(string texttoformat, string texttoput )
    {
            try
            {
                return string.Format(texttoformat, PlayerData.Playername);
            }
            catch (System.Exception)
            {

                return texttoformat;
            }
     }
       
        
    


    
    IEnumerator LetterAnimation(string TextToAnimate,Text WhereToPut,float letterSpeed)
    {
        WhereToPut.text = "";
        foreach (char letter in TextToAnimate.ToCharArray())
        {
            WhereToPut.text += letter;
            yield return new WaitForSeconds(letterSpeed);
        }
    }
    






}
