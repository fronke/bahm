using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float startingHealth = 100f;
    private float currentHealth;
    private bool isDead;


    private void Awake()
    {

    }


    void OnEnable()
    {
        // When the tank is enabled, reset the tank's health and whether or not it's dead.
        currentHealth = startingHealth;
        isDead = false;

        // Update the health slider's value and color.
        SetHealthUI();
    }


    public void TakeDamage(float amount)
    {
        // Reduce current health by the amount of damage done.
        currentHealth -= amount;

        // Change the UI elements appropriately.
        SetHealthUI();

        // If the current health is at or below zero and it has not yet been registered, call OnDeath.
        if (currentHealth <= 0f && !isDead)
        {
            OnDeath();
        }
    }


    void SetHealthUI()
    {

    }


    void OnDeath()
    {
        // Set the flag so that this function is only called once.
        isDead = true;

        // Turn the tank off.
        gameObject.SetActive(false);
    }
}
