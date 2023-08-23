using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MusicManager : MonoBehaviour
{
    public Slider soundSlider;
    public AudioListener soundLisner;


    private void Start()
    {
        if(!PlayerPrefs.HasKey("soundVolume"))
        {
            PlayerPrefs.SetFloat("soundVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }


    public void ChangeSound()
    {
        AudioListener.volume = soundSlider.value;
        Save();
    }


    private void Load()
    {
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("soundVolume", soundSlider.value);
    }


    

}
