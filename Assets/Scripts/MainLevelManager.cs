using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class MainLevelManager : MonoBehaviour
{

    public Button[] buttons;

    public GameObject progressBar;

    private void Awake()
    {
        int unlockLevel = PlayerPrefs.GetInt("unlockLevel", 1);
        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for(int i = 0;i<unlockLevel; i++)
        {
            buttons[i].interactable = true;
        }


    }

    public async void LoadScene(int sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
           
        await Task.Delay(100);
        //loadCanvas.SetActive(true);
        progressBar.SetActive(true);
        await Task.Delay(1500);
        
        scene.allowSceneActivation = true;
    }

    public void Level(int n)
    {
        LoadScene(n);
    }

    public void ExitGame()
    {
        Application.Quit();
    }




}
