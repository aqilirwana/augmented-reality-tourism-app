using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    // Define the delegate for the event
    public delegate void PlayerHealthChangedEventHandler(int currentHealth);

    // Define the event using the delegate
    public static event PlayerHealthChangedEventHandler OnPlayerHealthChanged;

    // Method to trigger the event
    public static void TriggerPlayerHealthChanged(int currentHealth)
    {
        if (OnPlayerHealthChanged != null)
        {
            OnPlayerHealthChanged(currentHealth);
        }
    }
}
