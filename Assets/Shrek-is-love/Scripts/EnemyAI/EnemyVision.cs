using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public EnemySettings settings;

    public bool IsPlayerVisible(out Vector3 playerPosition)
    {
        playerPosition = Vector3.zero;
        Collider[] players = Physics.OverlapSphere(transform.position, settings.viewRadius, settings.playerMask);

        foreach (var player in players)
        {
            Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < settings.viewAngle / 2)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (!Physics.SphereCast(transform.position, settings.sphereCastRadius, dirToPlayer, out _, distance, settings.obstacleMask))
                {
                    playerPosition = player.transform.position;
                    return true;
                }
            }
        }
        return false;
    }
}
