using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBootStrapScript : MonoBehaviour
{
    public TransitionHandler TransitionHandler;
    void Awake()
    {
        BootStrap.BootStrapScene();
        Screen.SetResolution(1280,720,false);
        
        
        
    }

    private void Update()
    {
        
        float loading = Time.timeSinceLevelLoad;
        if (loading > 5)
        {
            TransitionHandler.NextScene(2);
        }
    }

}
