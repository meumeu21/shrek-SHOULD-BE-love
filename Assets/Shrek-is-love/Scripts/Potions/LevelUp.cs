using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public HealthSystem healthSystem;
    public ManaSystem manaSystem;
    [SerializeField] int healthPoints = 40;
    [SerializeField] int manaPoints = 30;

    private void OnTriggerEnter(Collider other)
    {
        healthSystem = other.GetComponent<HealthSystem>();
        FindObjectOfType<AudioManager>().Play("Potion");
        UpgradeHealth();
        UpgradeMana();
        gameObject.SetActive(false);
    }

    void UpgradeHealth()
    {
        if (healthSystem != null)
        {
            healthSystem.UpgradeHealthBar(healthPoints);
        }
    }

    void UpgradeMana()
    {
        if (healthSystem != null)
        {
            manaSystem.UpgradeManaBar(manaPoints);
        }
    }
}