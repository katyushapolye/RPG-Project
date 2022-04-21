using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DialogueBlock
{
    public string DialogueBlockName;
    public string EndDialogueTrigger;
    public List<Dialogue> Dialogues;
    public GameObject MainActor;
    public GameObject SecondaryActor;



}
