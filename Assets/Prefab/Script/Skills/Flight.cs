using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flight : MonoBehaviour
{
    public float ascendForce = 4f;
    public float maxFlightTime = 2f; // Maximum time the player can fly
    private float flightTimer = 0f;

    private bool isFlying = false;
    private bool canFly = true; // Track if the player can fly
    private bool spaceKeyHeld = false; // Track if the space key is held down
    private float spaceKeyHoldTime = 0f; // Counter for space key hold time

    private Rigidbody rb;

    private Vector3 initialVelocity; // Store initial velocity before flying

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceKeyHeld = true;
            spaceKeyHoldTime = 0f;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            spaceKeyHeld = false;
            if (spaceKeyHoldTime >= 0.2f && canFly) // If held for more than 0.2 second and can fly, start flying
            {
                StartFlying();
            }
            spaceKeyHoldTime = 0f;
        }

        if (spaceKeyHeld)
        {
            spaceKeyHoldTime += Time.deltaTime;

            if (!isFlying && spaceKeyHoldTime >= 0.2f && canFly) // Check if space was held for more than 0.2 second and can fly
            {
                StartFlying();
            }
        }

        if (isFlying)
        {
            Fly();
        }

        // Update the flight timer if the player is flying
        if (isFlying)
        {
            flightTimer += Time.deltaTime;
            if (flightTimer >= maxFlightTime)
            {
                StopFlying();
            }
        }
        if (isJumping && canFly)
        {
            StartFlying();
        }
    }

    private void StartFlying()
    {
        isFlying = true;
        flightTimer = 0f;
        rb.useGravity = false;
        canFly = false; // Player can no longer fly
        initialVelocity = rb.velocity; // Store initial velocity before flying
        // Add any visual or audio effects here when flying starts
    }

    private void Fly()
    {
        rb.AddForce(Vector3.up * ascendForce, ForceMode.Force);

        // Maintain horizontal and vertical momentum
        rb.velocity = new Vector3(initialVelocity.x, rb.velocity.y, initialVelocity.z);
    }

    private void StopFlying()
    {
        isFlying = false;
        rb.useGravity = true;
        // Add any visual or audio effects here when flying stops
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canFly = true; // Player can fly again after touching the ground
        }
    }

    private bool isJumping = false; // Track if the player is transitioning from jumping to flying

    public void SetJumping(bool jumping)
    {
        isJumping = jumping;
    }
}
