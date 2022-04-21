using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public SceneHandler Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(SceneData.SceneCallBack[SceneManager.GetActiveScene().buildIndex] == false)
        {
            Debug.Log("Scene " + SceneManager.GetActiveScene().buildIndex.ToString() + " Booted");
            SceneData.SceneCallBack[SceneManager.GetActiveScene().buildIndex] = true;
            return;
        }
        if (SceneData.SceneCallBack[SceneManager.GetActiveScene().buildIndex] == true)
        {
            Debug.Log("Scene " + SceneManager.GetActiveScene().buildIndex.ToString() + " Booted Again");
            
        }


    }
    
}
