using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    public string closedStateName = "Closed"; // Change this to the closed state animation name
    public string openStateName = "Open";     // Change this to the open state animation name
    public float interactionDistance = 2f;    // Adjust the distance according to your preference

    private Animator animator;
    private bool isPlayerNearby = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false; // Disable the Animator at the start
    }

    private void Update()
    {
        // Check if the player is nearby and presses the "E" key
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            PlayFBXAnimation();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    public void PlayFBXAnimation()
    {
        animator.enabled = true; // Enable the Animator when you want to play the animation
        animator.Play(openStateName); // Play the "Open" animation
    }
}
