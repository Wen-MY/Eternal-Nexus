using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public int maxHealth = 100;
    public int damageOnTouch = 10;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die();
        }
        Debug.Log("Enemy health: " + currentHealth);

    }
    private void Die()
    {
        // Perform any death-related actions here (e.g., play death animation, drop items, etc.)
        Destroy(gameObject); // Destroy the bot GameObject when it dies
    }
}
