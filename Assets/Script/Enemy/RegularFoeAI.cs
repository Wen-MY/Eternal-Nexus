using UnityEngine;
using UnityEngine.AI;
public class RegularFoeAI : MonoBehaviour
{
    public Transform player;
    public FoeAttributes foeAttributes;
    public Animator animator;

    [SerializeField]private NavMeshAgent navMeshAgent;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float chasingStartTime;
    private float currentPatrolTime = 0f;
    private float currentPatrolDuration = 4f;

    private FoeAttributes.FoeState currentState = FoeAttributes.FoeState.Idle;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("Player GameObject not found!");
        }
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = foeAttributes.patrolSpeed;

        initialPosition = transform.position;
        targetPosition = GenerateRandomPatrolPosition();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current State : " + currentState);
        switch (currentState)
        {
            case FoeAttributes.FoeState.Idle:
                animator.SetBool("Idling",true);
                animator.SetBool("Walking", false);
                CheckPlayerAround();
                CheckDestroyable();
                break;
            case FoeAttributes.FoeState.Patrolling:
                animator.SetBool("Idling", false);
                animator.SetBool("Walking",true);
                animator.SetBool("Chasing", false);
                Patrol();
                break;
            case FoeAttributes.FoeState.Chasing:
                animator.SetBool("Idling", false);
                animator.SetBool("Walking", false);
                animator.SetBool("Chasing",true);
                Chase();
                break;
            case FoeAttributes.FoeState.Attacking:
                animator.SetBool("Chasing", false);
                animator.SetTrigger("Attacking");
                Attack();
                break;
  
        }
    }
    private void CheckDestroyable ()
    {
        if (Vector3.Distance(transform.position, player.position) > foeAttributes.destroyRange)
        {
            Destroy(gameObject);
        }
    }
    private void CheckPlayerAround()
    {
        if (Vector3.Distance(transform.position, player.position) <= foeAttributes.activateRange)
        {
            currentState = FoeAttributes.FoeState.Patrolling;
        }
        else
            currentState = FoeAttributes.FoeState.Idle;
    }
    private void Patrol()
    {
        navMeshAgent.destination = targetPosition;
        currentPatrolTime += Time.deltaTime;
        // If reached the target position, generate a new target position
        if (Vector3.Distance(transform.position, player.position) < 1 || currentPatrolTime >= currentPatrolDuration)
        {
            targetPosition = GenerateRandomPatrolPosition();
            currentPatrolTime = 0f;
        }
        CheckPlayerAround();
        // Transition to chasing state if player enters chaseRange
        if (Vector3.Distance(transform.position, player.position) <= foeAttributes.chaseRange) { 

            StartChasing();
        }
    }
    private Vector3 GenerateRandomPatrolPosition()
    {
        // Generate a random position within the patrolRange
        float randomX = Random.Range(initialPosition.x - foeAttributes.patrolRange, initialPosition.x + foeAttributes.patrolRange);
        float randomZ = Random.Range(initialPosition.z - foeAttributes.patrolRange, initialPosition.z + foeAttributes.patrolRange);
        return new Vector3(randomX, initialPosition.y, randomZ);
    }
    private void Chase()
    {
        // Update destination to player's position
        navMeshAgent.SetDestination(player.position);

        // Check if player is out of chase range or chasing timeout has passed
        if (Vector3.Distance(transform.position, player.position) > foeAttributes.chaseRange ||
            Time.time - chasingStartTime > foeAttributes.chasingTimeout)
        {
            StopChasing();
        }
        else if (Vector3.Distance(transform.position, player.position) <= foeAttributes.attackRange)
        {
            StartAttacking();
        }
    }

    private void Attack()
    {
        // attack behavior

        // Transition back to chasing state after attacking
        if (Vector3.Distance(transform.position, player.position) > foeAttributes.attackRange)
        {
            StartChasing();
        }
    }

    private void StartChasing()
    {
        currentState = FoeAttributes.FoeState.Chasing;
        navMeshAgent.speed = foeAttributes.chaseSpeed;
        chasingStartTime = Time.time;
    }

    private void StopChasing()
    {
        currentState = FoeAttributes.FoeState.Patrolling;
        navMeshAgent.speed = foeAttributes.patrolSpeed;
        navMeshAgent.ResetPath(); // Clear the current path
    }

    private void StartAttacking()
    {
        currentState = FoeAttributes.FoeState.Attacking;
        navMeshAgent.ResetPath(); // Stop moving while attacking
    }
}