using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState
{
    private readonly EnemyStateMachine stateMachine;
    private readonly NavMeshAgent agent;
    private readonly Animator animator;
    private readonly EnemySettings settings;

    private Transform player;
    private float waitTime;
    private bool caughtPlayer;

    public ChaseState(EnemyStateMachine stateMachine, NavMeshAgent agent, Animator animator, EnemySettings settings)
    {
        this.stateMachine = stateMachine;
        this.agent = agent;
        this.animator = animator;
        this.settings = settings;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Enter()
    {
        animator.SetBool("IsChasing", true);
        agent.isStopped = false;
        agent.speed = settings.speedRun;
        caughtPlayer = false;
    }

    public void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (!caughtPlayer)
        {
            float distanceToPlayer = Vector3.Distance(stateMachine.transform.position, player.position);

            agent.SetDestination(player.position);
            agent.isStopped = false;

            if (distanceToPlayer <= settings.attackRange)
            {
                caughtPlayer = true;
                stateMachine.SetState(EnemyState.Attack);
                return;
            }

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (distanceToPlayer >= settings.minChaseDistance)
                {
                    agent.isStopped = true;
                    waitTime -= Time.deltaTime;

                    if (waitTime <= 0 && distanceToPlayer >= settings.chaseStopDistance)
                    {
                        stateMachine.SetState(EnemyState.Patrol);
                    }
                }
            }
        }
    }
}
