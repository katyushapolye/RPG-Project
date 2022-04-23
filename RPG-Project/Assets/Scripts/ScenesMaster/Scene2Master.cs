using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Master : SceneMaster
{
    float timer;
    void Start()
    {
        timer = Time.deltaTime;
        Debug.Log(timer);

    }
     void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<TransitionHandler>().NextScene((int)SceneMaster.Sceneindex.CharCreation);
        }
        if (timer >= 40)
        {
            FindObjectOfType<TransitionHandler>().NextScene((int)SceneMaster.Sceneindex.CharCreation);
            Debug.Log(timer);
        }
        else
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
        }

    }

    public void SkipIntro()
    {
        FindObjectOfType<TransitionHandler>().NextScene((int)SceneMaster.Sceneindex.Muenzuka);
    }
}
