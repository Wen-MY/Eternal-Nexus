using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageActivator : MonoBehaviour
{
    public GameObject Boss;
    public GameObject LockStageBuilding;
    public Transform player;
    public CameraShake cam;
    public GameObject health;
    public float playerDetectionRange = 10f;
    public AudioClip bossMusic;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void checkSpawnable()
    {
        if (Vector3.Distance(transform.position, player.position) <= playerDetectionRange)
        {
            SoundManager.Instance.ChangeBGM(bossMusic);
            health.SetActive(true);
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
