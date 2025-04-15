using UnityEngine.AI;
using UnityEngine;
using System;

public class PatrolState
{
    private readonly EnemyStateMachine stateMachine;
    private readonly NavMeshAgent agent;
    private readonly Animator animator;
    private readonly EnemySettings settings;
    private readonly Transform[] waypoints;

    private int currentWaypointIndex;
    private float waitTime;

    public PatrolState(EnemyStateMachine stateMachine, NavMeshAgent agent, Animator animator, EnemySettings settings)
    {
        this.stateMachine = stateMachine;
        this.agent = agent;
        this.animator = animator;
        this.settings = settings;
        this.waypoints = stateMachine.GetComponent<EnemyAI>().waypoints;
    }

    public void Enter()
    {
        waitTime = settings.startWaitTime;
        agent.isStopped = false;
        agent.speed = settings.speedWalk;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
        
    }

    public void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 0.1f)
        {
            if (!agent.isStopped)
            {
                agent.isStopped = true;
                waitTime = settings.startWaitTime;
            }

            if (waitTime > 0)
            {
                waitTime -= Time.deltaTime;
            }
            else
            {
                NextPoint();
                agent.isStopped = false;
            }
        }
    }

    private void NextPoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }
}
