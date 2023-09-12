using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Foe",menuName ="Enemy/Foe Attributes")]
public class FoeAttributes : ScriptableObject
{
    public float chaseRange = 10f;
    public float attackRange = 2f;
    public float patrolSpeed = 2f;
    public float patrolRange = 5f;
    public float chaseSpeed = 4f;
    public float chasingTimeout = 10f;
    public float activateRange = 30f;
    public float destroyRange = 60f;

    public enum FoeState { Idle, Patrolling, Chasing, Attacking }
}
