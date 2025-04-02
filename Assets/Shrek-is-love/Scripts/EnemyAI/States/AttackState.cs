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

    private float attackCooldown;
    private bool isAttacking;

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
        attackCooldown = settings.attackWaitTime;
        isAttacking = false;
        TryAttack();
    }

    public void Update()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        stateMachine.transform.LookAt(new Vector3(playerPos.x, stateMachine.transform.position.y, playerPos.z));

        float distance = Vector3.Distance(stateMachine.transform.position, playerPos);

        if (distance > settings.attackRange * 1.2f)
        {
            stateMachine.SetState(EnemyState.Chase);
            return;
        }

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
        else if (!isAttacking)
        {
            TryAttack();
        }
    }

    private void TryAttack()
    {
        if (animator.GetBool("IsHit") || animator.GetBool("IsDead"))
            return;

        isAttacking = true;
        attackCooldown = settings.attackWaitTime;
        animator.SetTrigger("Attack");

        if (stateMachine.currentEnemy.CompareTag("BasicEnemy"))
        {
            CallAfterDelay.Create(0.55f, () =>
            {
                damageDealer?.Attack();
                isAttacking = false;
            });
        }
        else if (stateMachine.currentEnemy.CompareTag("MagicEnemy"))
        {
            magicSpell.Enter();
            isAttacking = false;
        }
    }
}