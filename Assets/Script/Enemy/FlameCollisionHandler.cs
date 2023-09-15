using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
public class FlameCollisionHandler : MonoBehaviour
{
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private float damagePerSecond = 5.0f;
    // Start is called before the first frame update
    private void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(targetTag))
        {
            Debug.Log(other.tag);
            // Check if the collided GameObject has the specified tag ("Player")
            // If it does, deduct health from the player
            HealthStaminaSystem playerHealth = other.GetComponent<HealthStaminaSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerSecond * Time.deltaTime * 50);
            }
        }
    }
   
}
