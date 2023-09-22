using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Slider healthBar; // Use Slider component for the health bar
    private float maxHealth;
    private float currentHealth;
    public GameObject boss; // Reference to the boss GameObject

    private void Start()
    {
        maxHealth = boss.GetComponentInChildren<Bot>().maxHealth;
    }

    private void Update()
    {
        currentHealth = boss.GetComponentInChildren<Bot>().getHealth();
        // Update the health bar using the value property
        healthBar.value = Mathf.Clamp01(currentHealth / maxHealth); // Ensure value stays within 0 to 1
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            //Debug.Log("Boss defeated!");
        }
    }
}
