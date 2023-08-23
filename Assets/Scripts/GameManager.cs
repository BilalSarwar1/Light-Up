using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        /*else
        {
            Destroy(instance);
        }*/
        
        DontDestroyOnLoad(instance);
    }


    public void NextLevel()
    {
        Debug.Log("Build Index: "+ SceneManager.GetActiveScene().buildIndex);
        if (SceneManager.GetActiveScene().buildIndex + 1 < 15)
        {
            Debug.Log("GameManage Called");
            int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
            LevelManager.Instance.LoadScene(nextScene);
        }
    }
}
