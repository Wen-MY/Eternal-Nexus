using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleporterManager : MonoBehaviour
{
    public GameObject teleporterPrefab;     // Prefab of the teleporter object
    public float teleportCooldown = 8.0f;   // Cooldown time for teleporting
    public float placeTeleporterCooldown = 5.0f; // Cooldown time for placing a teleporter
    private GameObject activeTeleporter;    // The currently active teleporter
    private bool isTeleporting;             // Flag to prevent multiple teleports
    private bool isPlacingTeleporter;       // Flag to prevent placing multiple teleporters
    private Camera playerCamera;            // Reference to the player's camera

    private void Start()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // Spawn or replace teleporter
        {
            SpawnOrReplaceTeleporter();
        }

        if (!isTeleporting && activeTeleporter != null && Input.GetKeyDown(KeyCode.F)) // Teleport using active teleporter
        {
            StartCoroutine(TeleportCooldown());
            TeleportUsingTeleporter();
        }
    }

    public void SpawnOrReplaceTeleporter()
    {
        if (!isPlacingTeleporter)
        {
            // Destroy the existing active teleporter
            if (activeTeleporter != null)
            {
                Destroy(activeTeleporter);
            }

            // Spawn a new teleporter in front of the camera with an additional 90-degree Y rotation
            Vector3 spawnPosition = playerCamera.transform.position + playerCamera.transform.forward * 2;
            Quaternion spawnRotation = Quaternion.LookRotation(playerCamera.transform.forward, Vector3.up) * Quaternion.Euler(0, 90, 0);

            // Remove vertical rotation from spawnRotation if looking up or down
            if (Mathf.Abs(playerCamera.transform.forward.y) < 0.9f)
            {
                spawnRotation = Quaternion.Euler(0, spawnRotation.eulerAngles.y, 0);
            }

            activeTeleporter = Instantiate(teleporterPrefab, spawnPosition, spawnRotation);

            // Start the cooldown for placing a teleporter
            StartCoroutine(PlaceTeleporterCooldown());
        }
    }

    private IEnumerator TeleportCooldown()
    {
        isTeleporting = true;
        yield return new WaitForSeconds(teleportCooldown);
        isTeleporting = false;
    }

    private IEnumerator PlaceTeleporterCooldown()
    {
        isPlacingTeleporter = true;
        yield return new WaitForSeconds(placeTeleporterCooldown);
        isPlacingTeleporter = false;
    }

    private void TeleportUsingTeleporter()
    {
        transform.position = activeTeleporter.transform.position;
    }
}
