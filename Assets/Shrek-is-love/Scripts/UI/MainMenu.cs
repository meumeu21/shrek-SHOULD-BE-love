using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button loadButton;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("MenuTheme");
        Cursor.lockState = CursorLockMode.Confined;
        if(DataPersistenceManager.instance.HasGameData())
        {
            loadButton.interactable = false;
        }
    }
    public void OnPlayButtonClicked() 
    {
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync("Level1");
        Time.timeScale = 1f;
    }

    public void OnLoadButtonClicked()
    {
        SceneManager.LoadSceneAsync("Level1");
        Debug.Log("LoadGame");
    }

    public void OnSettingsButtonClicked()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OnBackButtonClicked()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
