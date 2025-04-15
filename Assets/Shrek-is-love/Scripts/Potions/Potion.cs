using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Potion : MonoBehaviour, IDataPersistence
{
    [SerializeField] private string id;
    private bool collected = false;

    [ContextMenu("Generate guid for id")]

    private void Start()
    {
        // GenerateGuid();
    }
    private void GenerateGuid()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        collected = true;
    }

    public void LoadData(GameData gameData)
    {
        gameData.potionsCollected.TryGetValue(id, out collected);
        if (collected)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData gameData)
    {
        if(gameData.potionsCollected.ContainsKey(id)) 
        { 
            gameData.potionsCollected.Remove(id);
        }
        gameData.potionsCollected.Add(id, collected);
    }
}
