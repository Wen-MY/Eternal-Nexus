using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class Keypad : MonoBehaviour
{
    public string nextLevelName = "Level4";
    public TextMeshProUGUI Ans;
    public GameObject ammoManager;
    public GameObject passwordPanel;
    public GameObject inventoryCanvas;
    public GameObject crosshair;
    public GameObject playerMechanism;
    public GameObject pauseMenu;
    public TextMeshProUGUI messageText; // Reference to the UI Text element for the message
    public float activationRange = 3f; // Range at which the player can interact
    public GameObject player;
    private string Answer = "543210";
    private string enteredCode = "";
    private bool codeEntered = false;
    private bool isPlayerInRange = false;
    private bool isMessageShown = false;

    private void Start()
    {
        CloseKeyPad();
        messageText.enabled = false;

        // Call CheckPlayerInRange to initialize the message state
        CheckPlayerInRange();
    }

    private void Update()
    {
        CheckPlayerInRange();

        if (Input.GetKeyDown(KeyCode.O) && isPlayerInRange)
        {
            ToggleKeyPad();
        }

        if (pauseMenu.activeInHierarchy)
        {
            CloseKeyPad();
        }

        if (passwordPanel.activeInHierarchy && !codeEntered)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GetKeyboardInput();
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void GetKeyboardInput()
    {
        for (int i = 0; i <= 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                Number(i);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            RemoveLastDigit();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Enter();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseKeyPad();
        }
    }

    private void CheckPlayerInRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= activationRange)
        {
            if (!isPlayerInRange)
            {
                isPlayerInRange = true;
                ShowMessage("Press 'O' to open.");
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                if (isMessageShown)
                {
                    HideMessage();
                    ToggleKeyPad();
                }
                else if (!codeEntered)
                {
                    ToggleKeyPad();
                }
            }
        }
        else
        {
            if (isPlayerInRange)
            {
                isPlayerInRange = false;
                HideMessage();
            }
        }
    }

    public void ToggleKeyPad()
    {
        passwordPanel.SetActive(true);
        inventoryCanvas.SetActive(false);
        crosshair.SetActive(false);
        playerMechanism.SetActive(false);
        ammoManager.SetActive(false);
        Time.timeScale = 0;
    }

    public void CloseKeyPad()
    {
        passwordPanel.SetActive(false);
        inventoryCanvas.SetActive(true);
        crosshair.SetActive(true);
        playerMechanism.SetActive(true);
        ammoManager?.SetActive(true);
        Time.timeScale = 1;
    }

    public void Enter()
    {
        Debug.Log("Entered Code: " + enteredCode);
        Debug.Log("Answer: " + Answer);

        if (enteredCode.Equals(Answer))
        {
            HandleCorrectInput();
        }
        else
        {
            HandleIncorrectInput();
        }

        codeEntered = true;
    }

    public void Number(int number)
    {
        if (enteredCode.Length < Answer.Length)
        {
            enteredCode += number.ToString();
            Ans.text = new string('*', enteredCode.Length);
        }
    }

    public void RemoveLastDigit()
    {
        if (!string.IsNullOrEmpty(enteredCode))
        {
            enteredCode = enteredCode.Substring(0, enteredCode.Length - 1);
            Ans.text = Ans.text.Substring(0, Ans.text.Length - 1);
        }
    }

    private IEnumerator ClearEnteredCode(float delay)
    {
        yield return new WaitForSeconds(delay);
        Ans.color = Color.black;
        Ans.text = "";
        enteredCode = "";
        codeEntered = false;
    }

    private void HandleCorrectInput()
    {
        Ans.color = Color.green;
        Ans.text = "Correct";
        StartCoroutine(ClearEnteredCode(2f));

        // Load the next level after a delay (e.g., 2 seconds)
        StartCoroutine(LoadNextLevelWithDelay(2f));
    }

    private IEnumerator LoadNextLevelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextLevelName); // Load the next level
    }

    private IEnumerator CloseKeyPadWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CloseKeyPad();
    }

    private void HandleIncorrectInput()
    {
        Ans.color = Color.red;
        Ans.text = "Invalid";
        StartCoroutine(ClearMessage(2f)); // Hide the invalid message after 2 seconds
        StartCoroutine(CloseKeyPadWithDelay(1f));
    }

    private IEnumerator ClearMessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        Ans.color = Color.black;
        Ans.text = "";
    }

    private void ShowMessage(string message)
    {
        messageText.text = message;
        messageText.enabled = true;
        isMessageShown = true; // Set isMessageShown to true when showing the message
    }

    private void HideMessage()
    {
        messageText.enabled = false;
    }
}
