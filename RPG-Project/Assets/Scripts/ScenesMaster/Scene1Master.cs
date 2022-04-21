using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1Master : SceneMaster
{
    public TransitionHandler Transitionhandler;

    void Start()
    {
        FindObjectOfType<AudioHandler>().Play("MainMenu");
        Transitionhandler = FindObjectOfType<TransitionHandler>();
    }


    public void NextScene()
    {

        Transitionhandler.NextScene((int)SceneMaster.Sceneindex.CharCreation);
        PlayerData.CurrentHealth = 100;
        PlayerData.CurrentSanity = 100;
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("GameClosed");
        SceneManager.LoadScene(0);
    }

    public void ButtonClick()
    {
        FindObjectOfType<AudioHandler>().Play("Tick");
    }

    public void SetResolution(int mode)
    {
        switch (mode)
        {
            case 1:
                Screen.SetResolution(1600, 900, false);
                break;
            case 2:
                Screen.SetResolution(1280, 720, false);
                break;
            case 3:
                Screen.SetResolution(1024,576, false);
                break;



        }
    }
 }
