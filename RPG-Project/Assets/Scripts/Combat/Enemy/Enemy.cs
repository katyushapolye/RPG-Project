using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "BlankEnemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{


   
    public enum AIStrategy
    {
        Agressive,
        Passive,
        Random,
    }
    [SerializeField] protected string Name;
    [SerializeField] protected Sprite EnemySprite;
    [Range(1, 100)]
    [SerializeField] protected int SkillModifier;
    [Range(1, 20)]
    [SerializeField] protected int health;
    [Range(1, 20)]
    [SerializeField] protected int maxhealth;

    [SerializeField] protected AIStrategy AIstrategy;

    //Unity does not accept fixed size array in inspector
    private const int COUNT = 4;
    protected SpellCard[] grimoire =  new SpellCard[COUNT];


    public string getEnemyName() { return Name; }
    public Sprite getSprite() { return EnemySprite; }
    public void setHealth(int health)
    {
        this.health = health;
    }

    public int getSkillModifier(){return SkillModifier;}

    public int getHealth() { return health; }
    public int getMaxHealth() { return maxhealth;}
    public SpellCard[] getEnemyGrimoire() {return grimoire;}

    public AIStrategy getAIStrategy(){return AIstrategy;}

    public object CreateClone()
    {
        return this.MemberwiseClone();
    }





  
 
}
