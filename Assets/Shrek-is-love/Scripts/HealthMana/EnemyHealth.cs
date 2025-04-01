using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 30;
    private int currentHealth;
    [SerializeField] private Slider healthSlider; // Ссылка на UI Slider

    [SerializeField] private Animator animator;

    void Start()
    {
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
    }

    private void Die()
    {
        animator.SetTrigger("IsDead");
        CallAfterDelay.Create(3f, () =>
        {
            Destroy(gameObject);
        });
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }
}