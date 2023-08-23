using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeButtonScene : MonoBehaviour
{
    public void ChangeScene(int sceneName)
    {
        LevelManager.Instance.LoadScene(sceneName);
    }
}
