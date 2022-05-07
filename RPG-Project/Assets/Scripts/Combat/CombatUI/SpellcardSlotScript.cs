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
    [SerializeField] protected Text SpellcardClassText;
    [SerializeField] protected Text SpellcardModifierText;
    [SerializeField] protected Text SpellcardRawModifierPercentage;
    [SerializeField] protected GameObject DefensivePreset;
    [SerializeField] protected GameObject OffensivePreset;



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
            OffensivePreset.SetActive(false);
            DefensivePreset.SetActive(false);
            SpellcardNameText.text = null;
            SpellcardDescriptionText.text = null;
            SpellcardPatternText.text = null;
            SpellcardModifierText.text = null;
            SpellcardRawModifierPercentage.text = null;
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
            //We parse everything to the UI and then decide what we show or not
            SpellcardNameText.text = spellCard.name;
            SpellcardClassText.text = spellCard.spellClass.ToString();
            SpellcardDescriptionText.text = spellCard.spellDescription;
            SpellcardModifierText.text = spellCard.Modifiertype.ToString();
            SpellcardRawModifierPercentage.text = (spellCard.rawPercentageModifier*100).ToString() + "%";

            SpellcardPatternText.text = spellCard.pattern.ToString();
            spellButton.gameObject.SetActive(true);
            for (uint i = 0;i<spellCard.rawDamage;i++)
            {
                damageRatingStars[i].SetActive(true);
            }

            if(spellCard.spellClass == SpellCard.Class.Theatrical || spellCard.spellClass == SpellCard.Class.Offensive || spellCard.spellClass == SpellCard.Class.Forbidden || spellCard.spellClass == SpellCard.Class.Bug || spellCard.spellClass == SpellCard.Class.Slave)
            {
                OffensivePreset.SetActive(true);
                DefensivePreset.SetActive(false);
            }
            else if (spellCard.spellClass ==  SpellCard.Class.Dope || spellCard.spellClass == SpellCard.Class.Stress)
            {
                DefensivePreset.SetActive(true);
                OffensivePreset.SetActive(false);
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
