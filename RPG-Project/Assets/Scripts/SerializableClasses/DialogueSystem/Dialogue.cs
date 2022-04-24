using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string SpeakerName;
    public float letterSpeed;
    [TextArea(3,4)]
    public string DialogueText;
    public Sprite MainActorSprite;
    public Sprite SecondaryActorSprite;
    public string SceneTrigger;
    public string DialogueSoundName;



}
