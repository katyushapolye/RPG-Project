using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionHandler : MonoBehaviour
{
    public Animator transition;
    public Animator combatTransition;
    public float transitionTime = 1f; 
    
    
    //Have a look at couroutines and Sync this time with the animation is necessary??
    //Needs to be used to load a new scene instead of the default loadnextscene provided by unity
    //maybe automate the work of putting each scene manager as a variable on each masterscene script, buuuut, it might be a better idea to leave as it is

    public void NextScene(int sceneindex)
    {
        StartCoroutine(LoadLevel(sceneindex));
        

    }
    IEnumerator LoadLevel(int sceneindex)
    {
        transition.SetTrigger("EndScene");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneindex);

    }

    public void LoadCombatLevel()
    {
        
        StartCoroutine(LoadLevel((int)SceneMaster.Sceneindex.Combat));
    }


}
