using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public GameObject shieldPrefab;
    public float maxShieldHealth = 100f; // Maximum shield health
    public float shieldDuration = 10f;
    public float shieldCooldown = 15f;

    private GameObject currentShield;
    private bool isShieldActive = false;
    private float shieldEndTime;
    private float shieldHealth; // Current shield health

    private HealthStaminaSystem healthStaminaSystem;

    private void Start()
    {
        healthStaminaSystem = GetComponent<HealthStaminaSystem>();
        shieldHealth = maxShieldHealth;
    }

    private void Update()
    {
        // Check if the shield key is pressed, shield is not active, and cooldown is over
        if (Input.GetKeyDown(KeyCode.Q) && !isShieldActive && Time.time >= shieldEndTime)
        {
            ActivateShield();
        }

        // Check if shield is active and duration has passed
        if (isShieldActive && Time.time >= shieldEndTime)
        {
            BreakShield();
        }
    }

    private void ActivateShield()
    {
        currentShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity, transform);
        isShieldActive = true;
        shieldEndTime = Time.time + shieldDuration;
    }

    public void BreakShield()
    {
        if (currentShield != null)
        {
            Destroy(currentShield);
            currentShield = null;
            isShieldActive = false;
            StartCoroutine(ShieldCooldown());
        }
    }

    private IEnumerator ShieldCooldown()
    {
        yield return new WaitForSeconds(shieldCooldown);
        shieldEndTime = Time.time;
    }

    public bool IsShieldActive()
    {
        return isShieldActive;
    }

    // Call this function to deal damage to the shield
    public void TakeDamage(float damage)
    {
        if (isShieldActive)
        {
            healthStaminaSystem.TakeShieldDamage(damage); // Pass the damage to HealthStaminaSystem
        }
    }
}
