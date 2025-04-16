using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyAI : MonoBehaviour, IDataPersistence
{
    public EnemySettings settings;
    public Transform[] waypoints;

    private EnemyStateMachine stateMachine;
    private EnemyVision vision;
    private EnemyHealth health;

    [SerializeField] private int id;

    private void Awake()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        vision = GetComponent<EnemyVision>();
        health = GetComponent<EnemyHealth>();  
    }

    private void Update()
    {
        if (vision.IsPlayerVisible(out Vector3 playerPosition))
        {
            if (stateMachine.currentState == EnemyState.Patrol)
            {
                stateMachine.SetState(EnemyState.Chase);
            }
        }
    }

    public void LoadData(GameData gameData)
    {
        if (gameData.EnemyStatistics.TryGetValue(id, out EnemyData enemyData))
        {
            this.transform.position = enemyData.enemyPosition;
            health.currentHealth = enemyData.enemyHealth;
            health.isDead = enemyData.isDead;
            health.UpdateHealthUI();
            Debug.Log("Loaded statistics for " + id + " : position: " + gameData.EnemyStatistics[id].enemyPosition + " health: " + gameData.EnemyStatistics[id].enemyHealth);
        }
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.EnemyStatistics[id] =  new EnemyData(this.transform.position, health.currentHealth, health.isDead);
        Debug.Log("Saved statistics for " + id + " : position: " + gameData.EnemyStatistics[id].enemyPosition + " health: " + gameData.EnemyStatistics[id].enemyHealth);
    }
}

