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
    private float skillActivatedTime; // Time when the skill was last activated

    private void Start()
    {
        healthStaminaSystem = GetComponent<HealthStaminaSystem>();
        shieldHealth = maxShieldHealth;
        cooldownSlider.value = 1f; // Set to full (ready to use)
    }

    private void Update()
    {
        if (isSkillReady && !isShieldActive && Time.time >= shieldEndTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ActivateShield();
            }
        }

        if (isShieldActive && Time.time >= shieldEndTime)
        {
            BreakShield();
        }

        if (!isSkillReady)
        {
            float timeSinceActivation = Time.time - skillActivatedTime;

            if (timeSinceActivation <= shieldDuration)
            {
                // Skill is active, set slider to decrease over 10 seconds
                float sliderProgress = Mathf.Clamp01(1f - (timeSinceActivation / shieldDuration));
                cooldownSlider.value = sliderProgress;
            }
            else if (timeSinceActivation <= shieldDuration + shieldCooldown)
            {
                // Skill is in cooldown, reload the slider over 15 seconds
                float cooldownProgress = Mathf.Clamp01((timeSinceActivation - shieldDuration) / shieldCooldown);
                cooldownSlider.value = cooldownProgress;
            }
            else
            {
                // Skill is ready for use
                cooldownSlider.value = 1f;
                isSkillReady = true;
                Debug.Log("Skill is ready to use!");
            }
        }
    }

    private void ActivateShield()
    {
        currentShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity, transform);
        isShieldActive = true;
        shieldEndTime = Time.time + shieldDuration;
        skillActivatedTime = Time.time; // Record the time when the skill was activated
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
