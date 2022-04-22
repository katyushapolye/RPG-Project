using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpellcardSlotScript : MonoBehaviour
{

    //UI Stuff
    [SerializeField] protected Text SpellcardNameText;
    [SerializeField] protected Text SpellcardDescriptionText;
    [SerializeField] protected Text SpellcardPatternText;

    [SerializeField] protected Text SpellcardDamageStaticText;
    [SerializeField] protected Text SpellcardSpellPatternStaticText;

    public Button spellButton;





    private SpellCard spellCard;
    private CombatHandler combathandler;

    private GameObject[] damageRatingStars = new GameObject[10]; //hmm, maybe 10 stars is what we should use
    public GameObject ratingStarParent;
    
    // Start is called before the first frame update
    void Start()
    {
        combathandler = FindObjectOfType<CombatHandler>();
        for (int i = 0; i < ratingStarParent.transform.childCount; i++)
        {
            damageRatingStars[i] = ratingStarParent.transform.GetChild(i).gameObject;
        }
        foreach (GameObject g in damageRatingStars)
        {
            g.SetActive(false);
        }

        InitializeSpellcard();

    }

    void InitializeSpellcard()
    {
        if (spellCard == null)
        {
            SpellcardNameText.text = null;
            SpellcardDescriptionText.text = null;
            SpellcardPatternText.text = null;
            spellButton.gameObject.SetActive(false);
            foreach(GameObject g in damageRatingStars)
            {

                g.SetActive(false);
            }
            SpellcardDamageStaticText.gameObject.SetActive(false);
            SpellcardSpellPatternStaticText.gameObject.SetActive(false);
        }
        else
        {
            SpellcardNameText.text = spellCard.name;
            SpellcardDescriptionText.text = spellCard.spellDescription;
            SpellcardPatternText.text = spellCard.pattern.ToString();
            spellButton.gameObject.SetActive(true);
            for (uint i = 0;i<spellCard.rawDamage;i++)
            {
               

                damageRatingStars[i].SetActive(true);
            }


        }
        //Initializing object and setting up the UI
     


    }

    public void UseSpellcard()
    {
        combathandler.PlayerAttack(this.spellCard);


    }


    public void SetSpellCard(SpellCard spellcard)
    {
      
        this.spellCard = spellcard;
 
    }
}
