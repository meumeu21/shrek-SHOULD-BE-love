using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private UIManipulation UIManipulation;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void SaveGame()
    {
        DataPersistenceManager.instance.SaveGame();
        Debug.Log("The game is saved!");
    }

    public void LoadGame()
    {
        UIManipulation.RestartSequence();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        DataPersistenceManager.instance.LoadGame();
    }
    public void BackToGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        UIManipulation.RestartSequence();
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
