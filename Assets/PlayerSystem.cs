using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PlayerSystem : MonoBehaviour
    {
        public float maxHealth = 100f;
        public float maxStamina = 100f;
        public float currentHealth;
        public float currentStamina;
        PlayerMovement playerMovement;
        float sprintDecreaseRate = 20f; 
        float currentStaminaRegenerationDelay = 0f; 
        float staminaRegenerationDelay = 2f; //2 seconds then regenerate
        float staminaRegenerationRate = 2000f;
        public GunSystem gun;
        public Bot bot;
        public InventoryManager inventoryManager;
        public DemoScript demo;
        public Item item;
        void Start()
        {
            currentHealth = maxHealth;
            currentStamina = maxStamina;
            //healthBar.SetMaxHealth(maxHealth);
            playerMovement = GetComponent<PlayerMovement>();
        }

        void Update()
        {
            //Use Stamina
            if (playerMovement.currentState == PlayerMovement.MovementState.sprinting && currentStamina >= sprintDecreaseRate) {
                currentStamina -= sprintDecreaseRate * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
                Debug.Log("Stamina: "+currentStamina);
            }
            //Regenerate stamina
            if (playerMovement.currentState != PlayerMovement.MovementState.sprinting && currentStamina < maxStamina)
            {
                currentStaminaRegenerationDelay -= Time.deltaTime;
                if (currentStaminaRegenerationDelay <= 0f)
                {
                    currentStamina += staminaRegenerationRate * Time.deltaTime;
                    currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
                    currentStaminaRegenerationDelay = staminaRegenerationDelay;
                    Debug.Log("Stamina: "+currentStamina);
                }
            }
             

        }

        private void OnCollisionEnter(Collision collision) 
        {
            if (collision.gameObject.CompareTag("Enemy")) //if the player touch the enemies
            {
                Bot enemyBot = collision.gameObject.GetComponent<Bot>();
                if (enemyBot != null)
                {
                    TakeDamage(enemyBot.damageOnTouch); 
                }
            }
        }    
        public void UseStamina(float useStaminaAmount) {
            if (currentStamina > 100f) {
                currentStamina = 100f;
            }
            if (currentStamina - useStaminaAmount >= 0f) {
                currentStamina -= useStaminaAmount;
            }
            else {
                Debug.Log("Not enough stamina..."+ currentStamina);
            }
            Debug.Log("Stamina: "+ currentStamina);
        }
        
        public void TakeDamage(float damage) {
            currentHealth -= damage;
            //healthBar.SetHealth(currentHealth);
            if (currentHealth < 0f) {
                currentHealth = 0f;
                Die();
            }
            Debug.Log("Player health: "+ currentHealth);
        }

        public void TakeHeal(float healing) {
            currentHealth += healing;
            //healthBar.SetHealth(currentHealth);
            if (currentHealth>100f) {
                currentHealth = 100f;
            }
            Debug.Log("Player health: "+ currentHealth);
        }

        public void Boost(float addStamina) {
            currentStamina += addStamina;
            if (currentStamina>100f) {
                currentStamina = 100f;
            }
            Debug.Log("Stamina: "+ currentStamina);
        }

        void Die() {
            Destroy(gameObject);
        }
    }