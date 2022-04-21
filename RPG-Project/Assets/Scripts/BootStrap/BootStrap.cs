using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  static class BootStrap 
{
    public delegate void Bootstrap();
    public static Bootstrap BootStrapSequence;
    

    
    public static void BootStrapScene()
    {
        BootStrapSequence += BootStrapScene;
        Debug.Log(SceneManager.sceneCountInBuildSettings.ToString());
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            SceneData.SceneCallBack[i] = false;
            continue;
        }
    }
}
