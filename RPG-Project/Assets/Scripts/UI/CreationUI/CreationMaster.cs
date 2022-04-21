using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreationMaster : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<AudioHandler>().Play("CharCreation");
    }


    public void MaleSelect()
    {
        PlayerData.Sex = "M";
    }

    public void FemaleSelect()
    {
        PlayerData.Sex = "F";
    }

    public void NextScene()
    {
        FindObjectOfType<TransitionHandler>().NextScene(SceneManager.GetActiveScene().buildIndex + 1 ); //CHANGE LATER BASED ON WHAT SCENE WE WILL GO
    }

}
