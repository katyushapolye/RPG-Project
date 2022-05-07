using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "BlankSpellCard", menuName = "SpellCard")]

public class SpellCard: ScriptableObject { 
    public enum DamageType
    {
        Physical,
        Sanity,
    }
    public enum Class
    {
        //Will guide the player through the classification system later, but it well defined
        Null,
        //Based on the Grimoire of marisa's classification
        Theatrical, //Only for show, lots of pretty pattern
        Slave, //Invocation type,
        Stress, //debuff and offensive type, sanity damage and can have debuff
        Bug, //Swarm type
        Dope, //buff the user type
        //Based on my additional clasification
        Forbidden, //High density
        Offensive, //Direct fire card

        //Based on the Grimoire of marisa's classification

    }

    public enum Pattern //Will only be checked if card is of offensive nature
    {
        Null,
        Crossed,
        Straight,
        Ricochet,
        Circular,
        Laser,
        Random, 
    }

    public enum ModifierType //Will only be checked if card is of buff or debuff
    {
        Null,
        Evasion,
        Health,
        Sanity,
        Accuracy


    }

    //Serialize and privatise everything
    public string spellName;
    [TextArea(1,1)]
    public string spellDescription;
    public DamageType Damagetype;
    
    public ModifierType Modifiertype;
    [SerializeField] private Class spellClass;
    public int rawDamage; //Will only be checked if card is of offensive nature
    [Range(0, 1.0f)]
    public float rawPercentageModifier; //Will only be checked if card is of buff or debuff
    [Range(1, 10)]
    public int Size;
    public int Amount;
    public Pattern pattern;

    public Sprite danmakuSprite;



   


   

}
