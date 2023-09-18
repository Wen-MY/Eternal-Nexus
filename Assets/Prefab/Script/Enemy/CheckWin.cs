using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckWin : MonoBehaviour
{
    public GameObject Boss;
    public Transform player;
    public string levelSceneName;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        if(Boss.GetComponentInChildren<BossFoeAI>().Dead && player != null)
        {
            GameObject door = GameObject.Find("DOOR_Low");
            if (door != null)
            {
                // Set the local position and rotation of the door
                door.transform.localPosition = new Vector3(0.8f, 1.57f, 0.5f);
                door.transform.localRotation = Quaternion.Euler(0f, 90f, 0f);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(levelSceneName);
        }
    }
}
