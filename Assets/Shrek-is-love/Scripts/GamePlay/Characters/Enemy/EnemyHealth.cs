using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 30;
    public int currentHealth;
    public Slider healthSlider; // Ссылка на UI Slider

    [SerializeField] private Animator animator;

    private EnemyStateMachine stateMachine;
    public bool isDead = false;

    void Start()
    {
        if(isDead) { gameObject.SetActive(false); return; }
        stateMachine = GetComponent<EnemyStateMachine>();
        currentHealth = maxHealth;
        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
        }
        UpdateHealthUI();
    }


    public void TakeDamage(int damage)
    {
        animator.SetTrigger("IsHit");
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateHealthUI();
        if (currentHealth <= 0) Die();
        else stateMachine.SetState(EnemyState.Chase);
    }

    private void Die()
    {
        animator.SetTrigger("IsDead");
        CallAfterDelay.Create(3f, () =>
        {
            gameObject.SetActive(false);
        });
        isDead = true;
    }

    public void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }
}