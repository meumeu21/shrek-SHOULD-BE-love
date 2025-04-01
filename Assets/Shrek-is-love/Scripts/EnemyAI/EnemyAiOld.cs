using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiOld : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public float startWaitTime = 4;
    public float timeToRotate = 2;
    public float speedWalk = 6;
    public float speedRun = 9;

    public float viewRadius = 15;
    public float viewAngle = 90;
    public LayerMask playerMask; public LayerMask obstacleMask;
    public float meshResolution = 1f;
    public int edgeIterations = 4;
    public float edgeDistance = 0.5f;

    public Transform[] waypoints; int m_CurrentWaypointIndex;
    Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition;

    float m_WaitTime;

    float m_TimeToRotate;

    bool m_PlayerInRange;

    bool m_PlayerNear;

    bool m_IsPatrol;

    bool m_CaughtPlayer;

    private Animator animator;

    private DamageDealer damageDealer;

    void Start()
    {
        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_PlayerInRange = false;
        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;

        m_CurrentWaypointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);

        animator = GetComponent<Animator>();
        damageDealer = GetComponent<DamageDealer>();
    }

    void Update()
    {
        EnviromentView();

        animator.SetFloat("Speed", navMeshAgent.velocity.magnitude);

        if (!m_PlayerInRange && !m_CaughtPlayer)
        {
            m_IsPatrol = true;
        }


        if (!m_IsPatrol)
        {
            animator.SetBool("IsChasing", true);
            Chasing();
        }
        else
        {
            animator.SetBool("IsChasing", false);
            Patroling();
        }
    }



    private void Chasing()
    {
        m_PlayerNear = false;
        playerLastPosition = Vector3.zero;

        if (!m_CaughtPlayer)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

            if (distanceToPlayer <= 5f)
            {
                CaughtPlayer();
            }

            Move(speedRun);
            navMeshAgent.SetDestination(m_PlayerPosition);

            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (m_WaitTime <= 0 && !m_CaughtPlayer && distanceToPlayer >= 6f)
                {
                    m_IsPatrol = true;
                    m_PlayerNear = false;
                    Move(speedWalk);
                    m_TimeToRotate = timeToRotate;
                    m_WaitTime = startWaitTime;
                    navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                }
                else
                {
                    if (distanceToPlayer >= 2.5f)
                    {
                        Stop();
                        m_WaitTime -= Time.deltaTime;
                    }
                }
            }
        }
        else
        {
            // ≈сли враг атаковал > задержка перед преследованием
            m_WaitTime -= Time.deltaTime;
            if (m_WaitTime <= 0)
            {
                m_CaughtPlayer = false;
                Move(speedRun);
            }
        }
    }


    private void Patroling()
    {
        if (m_PlayerNear)
        {
            if (m_TimeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (m_WaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }

    void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }
    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }
    public void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }
    void CaughtPlayer()
    {
        m_CaughtPlayer = true;

        animator.SetTrigger("Attack");

        if (damageDealer != null)
        {
            damageDealer.Attack();
        }
    }
    void LookingPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.8)
        {
            if (m_WaitTime <= 0)
            {
                m_PlayerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = startWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (m_WaitTime < 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    m_WaitTime = Time.deltaTime;
                }
            }
        }
    }

    void EnviromentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
        m_PlayerInRange = false;
        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    m_PlayerInRange = true;
                    m_PlayerPosition = player.position;
                    m_IsPatrol = false;
                }
            }
        }

        // ≈сли игрок вне зоны видимости, возвращаемс€ в патрулирование
        if (!m_PlayerInRange)
        {
            m_IsPatrol = true;
        }
    }
}

