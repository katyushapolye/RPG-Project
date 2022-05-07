using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(fileName = "BlankCellphone", menuName = "Cellphone")]
public class Cellphone : Item
{
    public override void UseItem()
    {
        Debug.Log("Used Cellphone");
        try
        {
            if(PlayerData.GetCombatFlag() == false)
            {
                Debug.Log("Player Not Set correctly on intro dialogue");
                return;
            }
            FindObjectOfType<Scene4Manager>().StartIntroCombatDialogue();
            if (PlayerData.GetCurrentPlayerMainQuest() is WelcomeToTheEast)
            {
                PlayerData.GetCurrentPlayerMainQuest().Update();
            }
        }
        catch (System.Exception)
        {
            return;
            
        }
        
    }

}
