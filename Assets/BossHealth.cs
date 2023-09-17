using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Slider healthBar; // Use Slider component for the health bar
    public float maxHealth = 2500f;
    public float currentHealth;
    public GameObject boss; // Reference to the boss GameObject

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // Check if the boss GameObject is active
        if (boss != null)
        {
            if (boss.activeSelf)
            {
                healthBar.gameObject.SetActive(true);
            }
            else
            {
                healthBar.gameObject.SetActive(false);
            }
        }

        // Update the health bar using the value property
        healthBar.value = Mathf.Clamp01(currentHealth / maxHealth); // Ensure value stays within 0 to 1

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            BossDefeated();
        }
    }

    public void TakeDamage(float damage)
    {
        if (boss == null)
        {
            // Handle the case where the boss GameObject is destroyed
            return;
        }

        currentHealth -= damage;

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            BossDefeated();
        }

        Debug.Log("Boss health: " + currentHealth);
    }

    void BossDefeated()
    {
        Debug.Log("Boss defeated!");
        boss.SetActive(false);
        Destroy(boss);
    }
}
