using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnPlayButtonClicked() 
    {
        SceneManager.LoadSceneAsync("Level1");
        Time.timeScale = 1f;
    }

    public void OnSettingsButtonClicked()
    {
        Debug.Log("IS THAT THE ONE???");
    }
}
