using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public EnemySettings settings;
    public Transform[] waypoints;

    private EnemyStateMachine stateMachine;
    private EnemyVision vision;

    private void Awake()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        vision = GetComponent<EnemyVision>();
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
}

