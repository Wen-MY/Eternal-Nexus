using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

public class SpiderBoss : MonoBehaviour
{
    public float attackRange = 100f;
    public float shootCooldown = 3f;
    public float burstCooldown = 6f;
    public float jumpCooldown = 5f;
    public float jumpForce = 100f;
    public int maxHealth = 100;
    public float moveSpeed = 10f;
    public float attackDistance = 5f;
    public bool isChasing = false;
    public float chaseStartTime = 5f;
    public Transform bulletSpawnPoint; // The point where bullets will be spawned
    public GameObject bossHealth;
    public GameObject bulletPrefab; // The bullet prefab
    public float bulletSpeed = 45f; // Speed of the bullets
    public float shootDuration = 5f; // Duration of shooting in seconds
    public float timeBetweenShots = 0.2f; // Time between consecutive shots
    public int damagePerShot = 10; // Damage per shot
    public float jumpHeight = 10f; // The height of the jump
    public float jumpDuration = 1.5f; // The duration of the jump
    public float attackCooldown = 0.5f; // Cooldown between attacks in seconds
    private int attackCount = 0; // Counter for the number of attacks
    private float lastAttackTime = 0f; // Time when the last attack occurred
    public HealthStaminaSystem playerHealth;
    private Animator bossAnimator;

    private Transform player;
    private enum BossState { Idle, MovingToPlayer, Shooting, BurstShooting, Jumping, Attacking };
    private BossState currentState;

    private void Start()
    {
        bossHealth.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = BossState.Idle;
        bossAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        Debug.Log("Current State: " + currentState);

        


        switch (currentState)
        {
            case BossState.Idle:
                bossAnimator.SetTrigger("Idle"); // Trigger the "Idle" animation
                if (distanceToPlayer <= attackRange)
                {
                    bossHealth.SetActive(true);
                    float randomAction = Random.Range(0f, 1f);
                    if (randomAction <= 0.3f)
                    {
                        // Move to player
                        currentState = BossState.MovingToPlayer;

                    }
                    else if (randomAction <= 0.6f)
                    {
                        // Start shooting
                        currentState = BossState.Shooting;
                        StartCoroutine(Shoot());

                    }
                    else if (randomAction <= 0.9f)
                    {
                        // Start burst shooting
                        currentState = BossState.BurstShooting;
                        StartCoroutine(BurstShoot());

                    }
                    else
                    {
                        // Jump towards player
                        currentState = BossState.Jumping;
                        JumpTowardsPlayer();

                    }
                }
                break;

            case BossState.MovingToPlayer:
                bossAnimator.SetTrigger("MoveToPlayer"); // Trigger the "MoveToPlayer" animation
                if (distanceToPlayer <= attackDistance)
                {
                    currentState = BossState.Attacking;

                }

                if (!isChasing)
                {
                    StartChasing();
                }

                // Check if we've been chasing for 5 seconds
                if (Time.time - chaseStartTime >= 5f)
                {
                    isChasing = false; // Stop chasing
                    currentState = BossState.Idle;
                    return; // Exit the function
                }

                MoveTowardsPlayer();
                Debug.Log("Moving towards player...");
                break;

            case BossState.Shooting:
                bossAnimator.SetTrigger("Shooting"); // Trigger the "Shooting" animation
                // Shooting logic is handled in the Shoot coroutine
                break;

            case BossState.BurstShooting:
                // Burst shooting logic is handled in the BurstShoot coroutine
                bossAnimator.SetTrigger("BurstShooting"); // Trigger the "BurstShooting" animation
                break;

            case BossState.Jumping:
                bossAnimator.SetTrigger("Jumping"); // Trigger the "Jumping" animation
                // Jump logic is handled in the JumpTowardsPlayer method
                break;

            case BossState.Attacking:
                // Implement the attack logic here
                AttackPlayer();
                bossAnimator.SetTrigger("Attacking");
                break;

            default:
                break;
        }
    }



    private IEnumerator Shoot()
    {
        float shootEndTime = Time.time + shootDuration; // Calculate the end time of shooting
        while (Time.time < shootEndTime)
        {
            // Calculate the direction to the player
            Vector3 playerDirection = (player.position - bulletSpawnPoint.position).normalized;
            transform.LookAt(player);
            // Spawn a bullet
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(playerDirection));

            // Set bullet speed
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = playerDirection * bulletSpeed;

            // Wait for the next shot
            yield return new WaitForSeconds(timeBetweenShots);
        }

        // Transition to the Idle state after shooting is done
        currentState = BossState.Idle;
    }

    private IEnumerator BurstShoot()
    {
        for (int burstCount = 0; burstCount < 3; burstCount++)
        {
            if (currentState != BossState.BurstShooting)
                yield break;
            transform.LookAt(player);
            for (int i = 0; i < 3; i++)
            {
                // Calculate the direction to the player
                Vector3 playerDirection = (player.position - bulletSpawnPoint.position).normalized;

                // Spawn a bullet
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(playerDirection));

                // Set bullet speed
                Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
                bulletRigidbody.velocity = playerDirection * bulletSpeed;

                // Wait for the next shot
                yield return new WaitForSeconds(timeBetweenShots);
            }

            // Wait for a short cooldown between bursts
            yield return new WaitForSeconds(1.0f); // Adjust the delay between bursts
        }

        // Transition to the Idle state after all bursts are done
        currentState = BossState.Idle;
    }

    void JumpTowardsPlayer()
    {
        StartCoroutine(PerformJump());
    }

    private IEnumerator PerformJump()
    {
        // Record the start time and initial position before the jump
        float jumpStartTime = Time.time;
        Vector3 initialPosition = transform.position;

        // Calculate the target jump height (above the player's position)
        float targetJumpHeight = player.position.y + jumpHeight;

        // Calculate the jump duration for ascending and descending halves
        float ascendDuration = jumpDuration * 0.5f;
        float descendDuration = jumpDuration * 0.5f;

        // Perform the jump until the specified duration is reached
        while (Time.time - jumpStartTime < jumpDuration)
        {
            // Calculate the current progress of the jump (0 to 1)
            float jumpProgress = (Time.time - jumpStartTime) / jumpDuration;

            // Calculate the new height position based on the jump progress
            float newHeight;

            if (jumpProgress < 0.5f)
            {
                // Ascending phase: Go up smoothly
                newHeight = Mathf.Lerp(initialPosition.y, targetJumpHeight, jumpProgress * 2);
            }
            else
            {
                // Descending phase: Fall quickly
                float descendProgress = (jumpProgress - 0.5f) * 2;
                newHeight = Mathf.Lerp(targetJumpHeight, player.position.y, descendProgress);
            }

            // Update the boss AI's position (maintaining the X and Z coordinates)
            Vector3 newPosition = new Vector3(initialPosition.x, newHeight, initialPosition.z);
            transform.position = newPosition;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the boss AI reaches the ground at the player's position
        transform.position = new Vector3(player.position.x, player.position.y, player.position.z);

        // Transition to another state after the jump (e.g., Idle)
        currentState = BossState.Attacking;
    }



    void MoveTowardsPlayer()
    {
        // Calculate the direction to the player, ignoring the Y (vertical) component
        Vector3 playerDirection = (player.position - transform.position);
        playerDirection.y = 0f; // Set the Y component to 0

        // Normalize the direction
        playerDirection.Normalize();

        Vector3 moveVector = playerDirection * moveSpeed * Time.deltaTime;

        // Calculate the rotation to face the player horizontally
        Quaternion targetRotation = Quaternion.LookRotation(playerDirection);

        // Apply the horizontal rotation only (Y-axis)
        transform.rotation = Quaternion.Euler(0f, targetRotation.eulerAngles.y, 0f);

        // Move the boss
        transform.Translate(moveVector);

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackDistance)
        {
            // Trigger the attack function
            AttackPlayer();
        }
    }
    void StartChasing()
    {
        currentState = BossState.MovingToPlayer;
        chaseStartTime = Time.time; // Record the start time of the chase
        isChasing = true; // Set the flag to indicate chasing
    }


    void AttackPlayer()
    {
        // Check if the attack cooldown has passed and the attack count is less than 3
        if (Time.time - lastAttackTime >= attackCooldown && attackCount < 3)
        {
            // Implement the attack logic here
            // For example, you can play an attack animation, apply damage to the player,
            // and add any other attack-related behavior.
            float damageAmount = 5;
            // Check if the player is within the attack range (optional)
            if (playerHealth != null)
            {
                // Deal damage to the player
                playerHealth.TakeDamage(damageAmount); // Define 'damageAmount' as needed
            }

            // Update attack count and last attack time
            attackCount++;
            lastAttackTime = Time.time;

            // Check if the attack count reaches 3, then transition to another state
            if (attackCount == 3)
            {
                currentState = BossState.Idle; // Transition to an appropriate state (e.g., Idle)
                attackCount = 0; // Reset the attack count
            }
        }
    }
}
