using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PuzzleTable : MonoBehaviour
{
    public float activationRange = 3f;
    public TMP_Text messageText;
    public GameObject player; 
    private bool isInRange = false;

    private void Start()
    {
        messageText.enabled = false; 
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= activationRange)
        {
            if (!isInRange)
            { isInRange = true;
                messageText.text = "Press U to fix the circuit!";
                messageText.enabled = true;
            }

            if (Input.GetKeyDown(KeyCode.U)) {
                SceneManager.LoadScene("PuzzleScene");
            }
        }
        else
        {
            if (isInRange)
            {   isInRange = false;
                messageText.enabled = false;
            }
        }
    }
}