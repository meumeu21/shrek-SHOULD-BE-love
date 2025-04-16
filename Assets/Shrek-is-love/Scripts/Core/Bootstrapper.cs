using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour, IBootstrapper
{
    [SerializeField] private GameObject audioManagerPrefab;
    [SerializeField] private GameObject dataPersistenceManagerPrefab;

    private string sceneName;

    private void Awake()
    {
        Time.timeScale = 1f;
        sceneName = SceneManager.GetActiveScene().name;
        InstantiateAudioManager();
        InstantiatedataPersistenceManager();
        ConfigureDependencies();
    }

    public void InstantiateAudioManager()
    {
        Instantiate(audioManagerPrefab);
    }

    public void InstantiatedataPersistenceManager()
    {
        Instantiate(dataPersistenceManagerPrefab);
    }

    public void ConfigureDependencies()
    {
        if(sceneName == "MainMenu") { FindObjectOfType<AudioManager>().Play("MenuTheme"); }
        else { FindObjectOfType<AudioManager>().Play("MainTheme"); }
    }
}
