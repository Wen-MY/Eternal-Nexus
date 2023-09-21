using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossFoeAI : MonoBehaviour
{
    
    [Header("Short Distance Attack Parameters")]
    public float slamAttackRadius = 4f;
    public float slamAttackDamage = 50f;
    public float slamAttackRange = 30f;
    public float shortDistanceAttackRange = 5f;

    public float smashAttackDamage = 10f;
    public float smashForce = 35f;

    [Header("Long Distance Attack Parameters")]
    public float shootingRange = 100f;
    public float shootingInterval = 0.5f;
    public float projectileSpeed = 2f;

    [Header("Movement Parameters")]
    public float fovAngle = 180f;
    public float movingSpeed = 8f;

    [Header("Attack Mode Behavior")] //setting for let the boss have prefer which attack distance
    [Range(0f, 1f)]
    public float attackDistance = 0.7f; //the higher the value , the more prefer the boss using long distance attack
    public float decisionDuration = 5f; //a variable switch it decision

    [Header("Prefab using")]
    public GameObject bulletObject;
    public Transform firepoint;

    public GameObject grenadeObject;
    public Transform throwPoint;

    [Header("Boss State")]
    [SerializeField] private BossState currentState;

    [Header("Animation")]
    public Animator animator;
    
    [Header("Audio")]
    public AudioClip GrenadeThrowSound;
    public AudioClip WalkingSound;
    public AudioClip JumpSound;
    public AudioClip SmackSound;
    public AudioClip SlamSound;
    public AudioClip ShootingSound;

    public bool Dead = false;

    private enum BossState
    {
        longDistanceAttack,
        shortDistanceAttack,
        Idle,
    }

    private Vector3 toPlayer;
    private bool canAttack = true;

    private Transform player;
    private HealthStaminaSystem playerHealth;
    private NavMeshAgent navMeshAgent;

    private LayerMask obstacleLayer;
    void Start()
    {
        obstacleLayer = LayerMask.GetMask("Wall","Player");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerHealth = player.GetComponent<HealthStaminaSystem>();
        //JumpSlamAttack();
        StartCoroutine(DecisionRoutine());

    }

    // Update is called once per frame
    void Update()
    {
        if (!Dead)
        {
            Chase();
            PerformStateAction();
        }
        Die();
    }
    void FixedUpdate()
    {
        updateCurrentDistanceToPlayer();
    }

    /*BEHAVIOR-------------------------------------------------*/
    private IEnumerator DecisionRoutine()
    {
        while (true)
        {
            float decision = Random.value;

            // Determine the boss's behavior based on weighted probabilities
            if (decision <= attackDistance && toPlayer.magnitude > shortDistanceAttackRange)
            {
                currentState = BossState.longDistanceAttack;
            }
            else
            {
                currentState = BossState.shortDistanceAttack;
            }


            // Wait for a while before making the next decision
            yield return new WaitForSeconds(decisionDuration); // Adjust the delay as needed
        }
    }
    private void PerformStateAction()
    {
        float decision = Random.value;
        switch (currentState)
        {
            case BossState.longDistanceAttack:
                faceToPlayer();
                if (decision < 0.9) //may change to variable if needed , the boss now have fixed probability on choose shoot or
                    Shooting();
                else
                    throwingGrenade();
                break;
            case BossState.shortDistanceAttack:
                if (decision < 0.5 && toPlayer.magnitude > slamAttackRange)
                    checkSlamable();
                else
                    checkSmackable();
                break;
            case BossState.Idle:

                break;

        }
    }

    /*CHECK-------------------------------------------------*/
    private void updateCurrentDistanceToPlayer()
    {
        toPlayer = player.position - transform.position;
        //Debug.Log(toPlayer.magnitude);
    }

    
    private bool checkPlayerInFOV()
    {
        float angleToPlayer = Vector3.Angle(transform.forward, toPlayer);
        //if (!(angleToPlayer <= fovAngle * 0.5f)) Debug.Log("FOV PROBLEM");
        //if (!(toPlayer.magnitude <= shootingRange)) Debug.Log("SHOOTING RANGE PROBLEM");
        //if (checkIsObstacleBetween()) Debug.Log("OBSTABLE PROBLEM");
        Debug.Log(angleToPlayer <= fovAngle * 0.5f && toPlayer.magnitude <= shootingRange && !checkIsObstacleBetween());
        return (angleToPlayer <= fovAngle * 0.5f && toPlayer.magnitude <= shootingRange && !checkIsObstacleBetween());
    }
    private bool checkIsObstacleBetween()
    {

        RaycastHit hit;
        if (Physics.Raycast(transform.position, toPlayer, out hit, shootingRange, obstacleLayer))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red);
                return true; // Obstacle is between the bot and the player
            }
        }
        Debug.DrawRay(transform.position, toPlayer * shootingRange, Color.green);
        return false; // No obstacle or non-blocking obstacle
    }
    private void checkSmackable()
    {
        //Vector3 toPlayer = player.position - transform.position;
        float distanceToPlayer = toPlayer.magnitude;
        if (canAttack)
        {
            // Check if the player is within attack range
            if (distanceToPlayer <= shortDistanceAttackRange)
            {
                canAttack = false;  //prevent multiple attack one time
                animator.SetTrigger("Smack");
                Invoke("Smack", 0.5f);
            }
        }
    }
    private void checkSlamable()
    {
        if(toPlayer.magnitude > slamAttackRange && canAttack)
        {
            canAttack = false;
            animator.SetTrigger("JumpSlam");
            JumpSlamAttack();
        }
    }

    private void Die()
    {
        if(transform.GetComponentInChildren<Bot>().getHealth() <= 0)
        {
            transform.localRotation = Quaternion.Euler(90f,0f, 0f);
            animator.SetBool("Walking", false);
            Dead = true;
        }
    }
    /*ATTACK type-------------------------------------------------*/
    private void ResetAttack()
    {
        animator.ResetTrigger("Smack");
        animator.ResetTrigger("Shoot");
        animator.ResetTrigger("Throw");
        canAttack = true;
    }
    private void Smack()
    {
        // Calculate force direction to push the player away
        //Vector3 pushDirection = (player.position - transform.position).normalized;
        SoundManager.Instance.PlaySound(SmackSound);
        Vector3 pushDirection = toPlayer.normalized;
        Rigidbody playerRigidbody = player.GetComponent<Rigidbody>();
        if (playerRigidbody != null)
        {
            playerRigidbody.AddForce(pushDirection * smashForce, ForceMode.Impulse);
        }
        // Deal damage to the player
        playerHealth.TakeDamage(smashAttackDamage);
        //animator.SetBool("Smacking",true);
        Invoke("ResetAttack", 2f);
        Debug.Log("Enemy bot smashed the player!");
    }
    
    private void Slam()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, slamAttackRadius);
        SoundManager.Instance.PlaySound(SlamSound);
        foreach (Collider collider in colliders)
        {
            // Check if the collider belongs to an enemy or player (based on tags or components)
            if (collider.CompareTag("Player"))
            {
                CameraShake shake = collider.GetComponentInChildren<CameraShake>();
                playerHealth.TakeDamage(slamAttackDamage);
                shake.ShakeCamera(0.5f, 2f);
                
                Debug.Log("Enemy bot slammed player");
            }
        }
        Invoke("ResetAttack", 3f);

    }
    private void Shooting()
    {
        if (checkPlayerInFOV() && canAttack && toPlayer.magnitude > shortDistanceAttackRange)
        {
            canAttack = false;
            animator.SetTrigger("Shoot");
            animator.SetFloat("Shooting Speed", shootingInterval*2);
            SoundManager.Instance.PlaySound(ShootingSound);
            GameObject bullet = Instantiate(bulletObject, firepoint.position, firepoint.rotation);
            bullet.transform.localScale = Vector3.one * 0.1f;
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            
            bulletRb.velocity = toPlayer.normalized * toPlayer.magnitude;
            bulletRb.AddForce(toPlayer.normalized*projectileSpeed, ForceMode.Impulse);
            

            Destroy(bullet, 1f);

            Invoke("ResetAttack", shootingInterval);
        }
    }
    private void throwingGrenade()
    {
        if (checkPlayerInFOV() && canAttack && toPlayer.magnitude > shortDistanceAttackRange)
        {
            canAttack = false;
            animator.SetTrigger("Throw");
            GameObject grenade = Instantiate(grenadeObject, throwPoint.position, throwPoint.rotation);
            SoundManager.Instance.PlaySound(GrenadeThrowSound);
            Rigidbody grenadeRb = grenade.GetComponentInChildren<Rigidbody>();

            grenadeRb.velocity = toPlayer.normalized * toPlayer.magnitude;
            grenadeRb.drag = toPlayer.magnitude * 0.01f;
            Debug.Log(grenadeRb.drag);
            Invoke("ResetAttack", 3f);
        }
    }

    /*MOVEMENT-------------------------------------------------*/
    private void faceToPlayer()
    {
        if(toPlayer.magnitude > shortDistanceAttackRange*2)
        {
            transform.LookAt(player);
        }
    }
    private IEnumerator SimulateJump(Vector3 playerPosition,float high)
    {
        SoundManager.Instance.PlaySound(JumpSound);
        float jumpDuration = 1.0f;
        float jumpHeight = high;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = new Vector3(playerPosition.x, playerPosition.y + jumpHeight, playerPosition.z);

        float startTime = Time.time;
        while (Time.time - startTime < jumpDuration)
        {
            float t = (Time.time - startTime) / jumpDuration;
            float newX = Mathf.Lerp(startPosition.x, targetPosition.x, t);
            float newY = Mathf.Lerp(startPosition.y, targetPosition.y, t);
            float newZ = Mathf.Lerp(startPosition.z, targetPosition.z, t);
            transform.position = new Vector3(newX, newY, newZ);

            yield return null;
        }

        // Ensure the final position matches the target position exactly
        transform.position = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
        navMeshAgent.baseOffset = jumpHeight;


        //Simulate gravity
        jumpDuration = 0.2f;
        startPosition = transform.position;
        startTime = Time.time;
        while (Time.time - startTime < jumpDuration)
        {
            float t = (Time.time - startTime) / jumpDuration;
            float newX = Mathf.Lerp(startPosition.x, playerPosition.x, t);
            float newY = Mathf.Lerp(startPosition.y, playerPosition.y, t);
            float newZ = Mathf.Lerp(startPosition.z, playerPosition.z, t);
            transform.position = new Vector3(newX, newY, newZ);

            yield return null;
        }

        navMeshAgent.baseOffset = 0;
        Slam();

    }
    private void Chase()
    {
        // Update destination to player's position
        if ((toPlayer.magnitude > shortDistanceAttackRange && currentState == BossState.shortDistanceAttack) || !checkPlayerInFOV())
        {
            animator.SetBool("Walking", true);
            navMeshAgent.SetDestination(player.position);
            navMeshAgent.speed = movingSpeed;
            SoundManager.Instance.PlaySoundByInterval(WalkingSound,0.4f); //make it only play if is not playing
        }
        else
        {
            animator.SetBool("Walking", false);
            navMeshAgent.ResetPath();
        }
    }

    //ATTACK CALL
    public void JumpSlamAttack()
    {
        StartCoroutine(SimulateJump(player.position, 5f));
    }

    


}
