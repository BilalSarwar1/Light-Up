using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class LevelManager : MonoBehaviour
{
    public GameObject completePanel;
    public TextMeshProUGUI StageText;
    public bool isPaused = false;
    public static LevelManager Instance;
    

    //[SerializeField] private GameObject loadCanvas;
    [SerializeField] private GameObject progressBar;

    public GameObject menuPanel;
    
    public GameObject menuButton;
    public GameObject restartButton;

    private InputManager inputManager;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<InputManager>();

        isPaused = false;
        progressBar.SetActive(false);
        menuPanel.SetActive(false);
    }

    public void EnableMenuPanel()
    {
        menuButton.SetActive(false);
        restartButton.SetActive(false);
        isPaused = true;
        menuPanel.SetActive(true);
    }
    public void DisableMenuPanel()
    {
        menuButton.SetActive(true);
        restartButton.SetActive(true);
        menuPanel.SetActive(false);
        isPaused = false;
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

    public void Restart()
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex);
        new WaitForEndOfFrame();
        menuPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
    public void LoadMenuScene()
    {
        StageText.text = inputManager.LevelName;
        completePanel.SetActive(true);
    }

}