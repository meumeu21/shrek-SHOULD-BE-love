using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState { Patrol, Chase, Attack }

public class EnemyStateMachine : MonoBehaviour
{
    public EnemyState currentState { get; private set; }
    public GameObject currentEnemy { get; private set; }

    private PatrolState patrolState;
    private ChaseState chaseState;
    private AttackState attackState;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private EnemySettings settings;

    private void Awake()
    {
        currentEnemy = gameObject;

        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        settings = GetComponent<EnemyAI>().settings;

        patrolState = new PatrolState(this, navMeshAgent, animator, settings);
        chaseState = new ChaseState(this, navMeshAgent, animator, settings);
        attackState = new AttackState(this, navMeshAgent, animator, settings);

        SetState(EnemyState.Patrol);
    }

    public void SetState(EnemyState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case EnemyState.Patrol: patrolState.Enter(); break;
            case EnemyState.Chase: chaseState.Enter(); break;
            case EnemyState.Attack: attackState.Enter(); break;
        }
    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrol: patrolState.Update(); break;
            case EnemyState.Chase: chaseState.Update(); break;
            case EnemyState.Attack: attackState.Update(); break;
        }
    }
}
