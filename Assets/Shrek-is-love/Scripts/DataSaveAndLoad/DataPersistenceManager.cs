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

    private FileDataHandler fileDataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More that one DataManager on the scene");
        }

        instance = this; 

        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, FileName);
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

    private void Start()
    {
        // this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, FileName);
        // this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        // LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        this.gameData = fileDataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("No game is saved. Starting a new one.");
            NewGame();
        }

        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
        Debug.Log("Loaded HP = " + gameData.PlayerHP);
        Debug.Log("Loaded Mana = " + gameData.PlayerMana);
        Debug.Log("Loaded MaxHP = " + gameData.PlayerMaxHP);
        Debug.Log("Loaded MaxMana = " + gameData.PlayerMaxMana);
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        Debug.Log("Saved HP = " + gameData.PlayerHP);
        Debug.Log("Saved Mana = " + gameData.PlayerMana);
        Debug.Log("Saved MaxHP = " + gameData.PlayerMaxHP);
        Debug.Log("Saved MaxMana = " + gameData.PlayerMaxMana);

        fileDataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public void ResetGame() 
    {
        this.gameData = new GameData();
        fileDataHandler.Save(gameData);
        Debug.Log("IS THAT THE GRIM REAPER");
    } 

}
