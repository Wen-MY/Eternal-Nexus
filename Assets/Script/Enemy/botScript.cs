using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public int damageOnTouch = 20;
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
    }
    private void Die()
    {
        // Perform any death-related actions here (e.g., play death animation, drop items, etc.)
        Destroy(gameObject); // Destroy the bot GameObject when it dies
    }
}
