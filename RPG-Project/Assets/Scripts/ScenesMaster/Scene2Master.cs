using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Master : SceneMaster
{
 
    public void SkipIntro()
    {
        FindObjectOfType<TransitionHandler>().NextScene((int)SceneMaster.Sceneindex.Muenzuka);
    }
}
