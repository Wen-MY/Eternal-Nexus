using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageActivator : MonoBehaviour
{
    public GameObject Boss;
    public GameObject LockStageBuilding;
    public Transform player;
    public CameraShake cam;
    public float playerDetectionRange = 10f;
    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void checkSpawnable()
    {
        if (Vector3.Distance(transform.position, player.position) <= playerDetectionRange)
        {
            Boss.SetActive(true);
            cam.ShakeCamera(1, 1);
            LockStageBuilding.SetActive(true);
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        checkSpawnable();
    }
}
