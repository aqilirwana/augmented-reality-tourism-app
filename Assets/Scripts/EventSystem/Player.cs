using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Trigger the event
        EventManager.TriggerPlayerHealthChanged(currentHealth);

        if (currentHealth <= 0)
        {
            // Handle player death
            Debug.Log("Player is dead");
        }
    }
}
