using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class HealthStaminaSystem : MonoBehaviour
    {
        public UnityEngine.UI.Image healthBar;
        public UnityEngine.UI.Slider staminaBar;
        public float maxHealth = 100f;
        public float maxStamina = 100f;
        public float currentHealth;
        public float currentStamina;
        private float dValue = 5;
        public Bot bot;
        public GameOverScreen gameOverScreen;
        public GameObject gunHolder;
       
        void Start()
        {
            currentHealth = maxHealth;
            currentStamina = maxStamina;
            staminaBar.maxValue = currentStamina;
        }

        void Update()
        {
            //health
            healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
                gameOverScreen.GameOver();
                Debug.Log("Dead");
            }
            
            //use stamina
            if (Input.GetButton("Sprint") && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
            {
                DecreaseEnergy();
            }
            else if (currentStamina != maxStamina)
            {
                IncreaseEnergy();
            }
            staminaBar.value = currentStamina;

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


    private void DecreaseEnergy()
    {
        if (currentStamina != 0)
        {
            currentStamina -= dValue * Time.deltaTime;
        }
        if (currentStamina <= -1)
        {
            currentStamina = 0;
        }
    }

    public void IncreaseEnergy()
    {
        currentStamina += dValue * Time.deltaTime;
        if (currentStamina >= maxStamina)
        {
            currentStamina = maxStamina;
        }
    }

    void TakeDamage(float damage) {
            currentHealth -= damage;
            //healthBar.SetHealth(currentHealth);
            if (currentHealth < 0f) {
                currentHealth = 0f;
            }
            Debug.Log("Player health: "+ currentHealth);
        }

        public void RecoverHealth(float healing) {
            currentHealth += healing;
            //healthBar.SetHealth(currentHealth);
            if (currentHealth>100f) {
                currentHealth = 100f;
            }
            Debug.Log("Player health: "+ currentHealth);
        }
    }