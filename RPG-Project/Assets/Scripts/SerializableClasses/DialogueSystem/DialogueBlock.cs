using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

//Will not be deriving from scriptable object, as it would mean that we would lose the control over the gameObjects we would put in it, because it would mean
//the dialogue would be scene independent, might look at it to make it work later but for now, its better to say that way.
public class DialogueBlock
{
    public string DialogueBlockName;
    public string EndDialogueTrigger;
    public List<Dialogue> Dialogues;
    public GameObject MainActor;
    public GameObject SecondaryActor;



}
