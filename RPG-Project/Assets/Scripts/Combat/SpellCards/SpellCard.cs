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

    public enum Pattern
    {
        Crossed,
        Straight,
        Ricochet,
        Circular,
        Random, 
    }


    public string spellName;
    [TextArea(1,1)]
    public string spellDescription;
    public DamageType Damagetype;
    public int rawDamage;
    [Range(1, 10)]
    public int Size;
    public int Amount;
    public Pattern pattern;

    public Sprite danmakuSprite;



   


   

}
