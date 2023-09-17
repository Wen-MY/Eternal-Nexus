using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damageAmount = 100f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name);

                // Check if the hit object is tagged as "Boss"
                if (hit.collider.CompareTag("Boss"))
                {
                    Debug.Log("Boss found: " + hit.collider.gameObject.name);
                    BossHealth bossHealth = hit.collider.GetComponent<BossHealth>();
                    if (bossHealth != null)
                    {
                        bossHealth.TakeDamage(damageAmount);
                        Debug.Log("Boss health after attack: " + bossHealth.currentHealth);
                    }
                    else
                    {
                        Debug.LogError("BossHealth component not found on hit object.");
                    }
                }
                else
                {
                    Debug.Log("No Boss tag found on hit object.");
                }
            }
            else
            {
                Debug.Log("No hit detected.");
            }
        }
    }
}
