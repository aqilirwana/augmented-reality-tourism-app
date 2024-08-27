using TMPro;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    private void OnEnable()
    {
        EventManager.OnPlayerHealthChanged += UpdateHealthDisplay;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerHealthChanged -= UpdateHealthDisplay;
    }

    private void UpdateHealthDisplay(int currentHealth)
    {
        healthText.text = "Health: " + currentHealth.ToString();
    }
}
