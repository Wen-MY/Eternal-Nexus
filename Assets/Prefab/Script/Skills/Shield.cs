using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public GameObject shieldPrefab;
    public float maxShieldHealth = 100f;
    public float shieldDuration = 10f;
    public float shieldCooldown = 15f;

    private GameObject currentShield;
    private bool isShieldActive = false;
    private float shieldEndTime;
    private float shieldHealth;

    private HealthStaminaSystem healthStaminaSystem;
    public Slider cooldownSlider;

    private bool isSkillReady = true; // Track if the skill is ready
    private float cooldownStartTime; // Time when the cooldown starts

    private void Start()
    {
        healthStaminaSystem = GetComponent<HealthStaminaSystem>();
        shieldHealth = maxShieldHealth;
        cooldownSlider.value = 1f; // Set to full (ready to use)
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isSkillReady)
        {
            ActivateShield();
        }

        if (isShieldActive && Time.time >= shieldEndTime)
        {
            BreakShield();
            isSkillReady = true; // Set the skill as ready
            Debug.Log("Skill is ready to use!");
        }

        if (!isShieldActive)
        {
            // Calculate the cooldown progress based on the cooldown start time
            float cooldownProgress = Mathf.Clamp01((Time.time - cooldownStartTime) / shieldCooldown);
            cooldownSlider.value = 1f - cooldownProgress;
        }
    }

    private void ActivateShield()
    {
        currentShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity, transform);
        isShieldActive = true;
        shieldEndTime = Time.time + shieldDuration;
        cooldownStartTime = shieldEndTime + 10f; // Start cooldown after 10 seconds
        isSkillReady = false; // Skill is not ready during cooldown
        Debug.Log("Shield activated.");
    }

    public void BreakShield()
    {
        if (currentShield != null)
        {
            Destroy(currentShield);
            currentShield = null;
            isShieldActive = false;
        }
    }

    public bool IsShieldActive()
    {
        return isShieldActive;
    }

    public void TakeDamage(float damage)
    {
        if (isShieldActive)
        {
            healthStaminaSystem.TakeShieldDamage(damage);
        }
    }
}
