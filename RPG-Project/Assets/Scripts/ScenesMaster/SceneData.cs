using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public static class SceneData
{
    //Will store non-pensistent scene data, such as first callback for certain scene animation or non-static npcs on it.
    public static Dictionary<int,bool> SceneCallBack = new Dictionary<int,bool>();
       
}
