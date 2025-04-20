using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string FileName;

    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;

    private IDataRepository dataRepository;

    public static DataPersistenceManager instance { get; private set; }

    public void Initialize(IDataRepository repository)
    {
        this.dataRepository = repository;
    }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More that one DataManager on the scene. The newest one was destroyed.");
            Destroy(this.gameObject);
            return;
        }

        instance = this; 
        DontDestroyOnLoad(this.gameObject);

        Initialize(new FileDataHandler(Application.persistentDataPath, FileName));
    }

    private void OnEnable() 
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable() 
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene) 
    {
        SaveGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = dataRepository.Load();

        if (this.gameData == null)
        {
            Debug.LogWarning("No game is saved. A new game needs tobe started.");
            return;
        }

        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        if (this.gameData == null)
        {
            Debug.LogWarning("No data to save.");
            return;
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }


        dataRepository.Save(gameData);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public void ResetGame() 
    {
        this.gameData = new GameData();
        dataRepository.Save(gameData);
        Debug.Log("IS THAT THE GRIM REAPER");
    } 

    public bool HasGameData()
    {
        return gameData != null;
    }
}
