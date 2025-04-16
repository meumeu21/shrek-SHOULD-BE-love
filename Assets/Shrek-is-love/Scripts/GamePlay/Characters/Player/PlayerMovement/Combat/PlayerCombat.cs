using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private int physAttackPoints = 5;
    [SerializeField] private float physAttackRadius = 5f;
    [SerializeField] private int yellAttackPoints = 2;
    [SerializeField] private float yellAttackRadius = 10f;
    [SerializeField] private int manaPoints = 5;

    private PlayerAnimationController _animationController;
    private IManaSystem _manaSystem;

    public void Initialize(IManaSystem manaSystem, PlayerAnimationController animationController)
    {
        _animationController = animationController;
        _manaSystem = manaSystem;
    }

    private void Update()
    {
        PhysAttack();
        YellAttack();

    }

    private void PhysAttack()
    {
        if (Input.GetMouseButtonDown(0) && !_animationController.IsAnimationPlaying("Attacking", 1) && !_animationController.IsAnimationPlaying("Yelling", 1) && _animationController.isGrounded())
        {
            _animationController.PlayAttack();
            CallAfterDelay.Create(0.5f, () =>
            {
                DealDamageToEnemies(physAttackPoints, "Attack");
            });

        }

    }
    private void YellAttack()
    {
        if (Input.GetMouseButtonDown(1) && !_animationController.IsAnimationPlaying("Yelling", 1) && !_animationController.IsAnimationPlaying("Attacking", 1) && _animationController.isGrounded())
        {
            if (_manaSystem != null && _manaSystem.GetCurrentMana() > 0)
            {
                if (_manaSystem.UseSufficientMana(manaPoints))
                {
                    _animationController.PlayYell();
                    FindObjectOfType<AudioManager>().Play("Scream");
                    DealDamageToEnemies(yellAttackPoints, "Yell");
                }
            }
        }

    }

    private void DealDamageToEnemies(int damage, string attackType)
    {
        float attackRadius = (attackType == "Attack") ? physAttackRadius : yellAttackRadius;
        LayerMask enemyLayer = LayerMask.GetMask("Enemy");

        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRadius, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Debug.Log($"Игрок нанес {damage} врагу {enemy.name} (Тип атаки: {attackType})");
            }
        }
    }
}
