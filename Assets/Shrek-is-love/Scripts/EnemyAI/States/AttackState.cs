using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState
{
    private readonly EnemyStateMachine stateMachine;
    private readonly NavMeshAgent agent;
    private readonly Animator animator;
    private readonly EnemySettings settings;
    private readonly DamageDealer damageDealer;
    private readonly MagicSpell magicSpell;

    private float waitTime;

    public AttackState(EnemyStateMachine stateMachine, NavMeshAgent agent, Animator animator, EnemySettings settings)
    {
        this.stateMachine = stateMachine;
        this.agent = agent;
        this.animator = animator;
        this.settings = settings;
        damageDealer = stateMachine.GetComponent<DamageDealer>();
        magicSpell = stateMachine.GetComponent<MagicSpell>();
    }

    public void Enter()
    {
        agent.isStopped = true;
        animator.SetTrigger("Attack");
        if (stateMachine.currentEnemy.CompareTag("BasicEnemy")) {
            CallAfterDelay.Create(0.3f, () =>
            {
                damageDealer?.Attack();
            });
        }
        if (stateMachine.currentEnemy.CompareTag("MagicEnemy"))
        {
            magicSpell.Enter();
        }
        waitTime = settings.attackWaitTime;
    }
    public void Update()
    {
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            stateMachine.SetState(EnemyState.Chase);
        }
    }
}