using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMaster : MonoBehaviour
{//This Class will Be Used As A Father-Class for all MasterScenes
    public UIMaster UIMasterVar;
    public TransitionHandler TransitionHandler;
    public enum Sceneindex
    {
        Boostrap,
        Combat,
        Menu,
        Intro,
        CharCreation,
        Muenzuka
    }

    private void Awake()
    {
        try
        {
            UIMasterVar = FindObjectOfType<UIMaster>();
            TransitionHandler = FindObjectOfType<TransitionHandler>();
        }
        catch (System.Exception)
        {

           
        }
        
    }

    public virtual void UpdateSceneTrigger(string SceneTrigger)
    {
        Debug.Log("Error, This SceneTrigger Was Not Overwritten");
    }



}
