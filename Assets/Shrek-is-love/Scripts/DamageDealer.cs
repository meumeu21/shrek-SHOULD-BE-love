using System.Collections;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public float attackRadius = 5f; 
    public LayerMask playerLayer;
    public int damageAmount = 10;
    public float attackCooldown = 1.5f; 
    private bool canAttack = true; 

    // private void OnTriggerEnter(Collider other)
    // {
    //     HealthSystem healthSystem = other.GetComponent<HealthSystem>();
    //     if (healthSystem != null)
    //     {
    //         healthSystem.TakeDamage(damageAmount);
    //     }
    // }

    public void Attack()
    {
        if (!canAttack) return;

        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        canAttack = false; 

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius, playerLayer);
        
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                HealthSystem playerHealth = hitCollider.GetComponent<HealthSystem>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                    Debug.Log("Игрок получил урон: " + damageAmount);
                }
            }
        }

        yield return new WaitForSeconds(attackCooldown); 
        canAttack = true; 
    }
}