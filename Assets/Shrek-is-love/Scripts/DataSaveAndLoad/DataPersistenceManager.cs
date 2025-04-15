using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string FileName;

    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;

    private FileDataHandler fileDataHandler;

    public static DataPersistenceManager instance { get; private set; } // полкчать публично, изменять приватно

    private void Awake()
    {
        // должен быть только 1 менеджер
        if(instance != null)
        {
            Debug.LogError("More that one DataManager on the scene");
        }

        instance = this; 
    }

    private void Start()
    {
        this.fileDataHandler = new FileDataHandler("C:/Users/Marley/Documents/UnityProjects/shrek-SHOULD-BE-love/Assets/Shrek-is-love/Scripts/DataSaveAndLoad/SaveData", FileName); // Application.persistentDataPath - даст стандартную директорию для сохранения
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
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

}
