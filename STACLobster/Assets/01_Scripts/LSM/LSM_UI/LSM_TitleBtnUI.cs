using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LSM_TitleBtnUI : MonoBehaviour
{
    
    public void OnGameStart()
    {
        SceneManager.LoadScene(0);
    }

    public void OnSetting()
    {

    }

    public void OnExit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

}
