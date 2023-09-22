using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Configuration")]
    public float damage = 20f;
    public float explodeForce = 10f;
    public float explodeRadius = 5f;
    public float explosionDelay = 0.8f;


    [Header("References")]
    public GameObject explosionEffect;
    public AudioClip explosionSound;

    private bool hasExploded = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (!hasExploded)
        {
            StartCoroutine(Explode());
        }
    }
    private IEnumerator Explode()
    {
        yield return new WaitForSeconds(explosionDelay);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explodeRadius);
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        SoundManager.Instance.PlaySound(explosionSound);
        foreach (Collider collider in colliders)
        {
            // Check if the collider belongs to an enemy or player (based on tags or components)
            if (collider.CompareTag("Player"))
            {
                HealthStaminaSystem playerHealth = collider.GetComponent<HealthStaminaSystem>();
                CameraShake shake = collider.GetComponentInChildren<CameraShake>();
                Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
                playerHealth.TakeDamage(damage);
                shake.ShakeCamera(0.5f, 0.5f);
                rigidbody.AddForce((rigidbody.position - transform.position) * explodeForce, ForceMode.Impulse);
                Debug.Log("Player being damaged by Grenade");
            }
        }
        Destroy(explosion, 1f);
        Destroy(transform.parent.gameObject);

        hasExploded = true; // Set a flag to prevent multiple explosions
    }
}
