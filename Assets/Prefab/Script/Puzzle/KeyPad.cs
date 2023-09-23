using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class KeyPad : MonoBehaviour
{
    public string nextLevelName = "Level4";
    public TextMeshProUGUI Ans;
    public TextMeshProUGUI messageText; // Reference to the UI Text element for the message
    public float activationRange = 3f; // Range at which the player can interact
    public GameObject player;
    public GameObject pauseMenu;
    public GameObject playerMechanism;
    public GameObject crosshair;
    public GameObject ammoManager;
    public GameObject passwordPanel;
    //public GameObject shield;
    public WeaponSwitcher weaponSwitcher;
    public InventoryManager inventoryManager;
    public FiringSystem firingSystem;
    private string Answer = "505812";
    private string enteredCode = "";
    private bool codeEntered = false;
    private bool isPlayerInRange = false;
    private bool isMessageShown = false;

    private void Start()
    {
        Debug.Log("Keypad script started.");
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        weaponSwitcher = GameObject.Find("Gun Holder").GetComponent<WeaponSwitcher>();
        messageText = GameObject.Find("Instruction/MessageText").GetComponent<TextMeshProUGUI>();
        messageText.enabled = false;
        CloseKeyPad();
        // Call CheckPlayerInRange to initialize the message state
        CheckPlayerInRange();
    }

    private void Update()
    {
        Debug.Log("Update() is called.");
        CheckPlayerInRange();
        firingSystem = GameObject.Find("Mechanism").GetComponent<FiringSystem>();
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
        }
    }

    private void GetKeyboardInput()
    {
        for (int i = 0; i <= 9; i++)
        {
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
        Debug.Log("CheckPlayerInRange() is called.");
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
        Debug.Log("ToggleKeyPad() is called.");
        weaponSwitcher.enabled = false;
        inventoryManager.enabled = false;
        firingSystem.enabled = false;

        passwordPanel.SetActive(true);
        //shield.SetActive(false);
        playerMechanism.SetActive(false);
        crosshair.SetActive(false);
        playerMechanism.SetActive(false);
        ammoManager.SetActive(false);
        Time.timeScale = 0;
    }

    public void CloseKeyPad()
    {
        Debug.Log("CloseKeyPad() is called.");
        weaponSwitcher.enabled = true;
        inventoryManager.enabled = true;
        firingSystem.enabled = true;

        passwordPanel.SetActive(false);
        //shield.SetActive(true);
        playerMechanism.SetActive(true);
        crosshair.SetActive(true);
        playerMechanism.SetActive(true);
        ammoManager.SetActive(true);
        Time.timeScale = 1;
    }

    public void Enter()
    {
        Debug.Log("Enter() is called.");
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
        Debug.Log("Number(" + number + ") is called.");
        if (enteredCode.Length < Answer.Length)
        {
            enteredCode += number.ToString();
            Ans.text = new string('*', enteredCode.Length);
        }
    }

    public void RemoveLastDigit()
    {
        Debug.Log("RemoveLastDigit() is called.");
        if (!string.IsNullOrEmpty(enteredCode))
        {
            enteredCode = enteredCode.Substring(0, enteredCode.Length - 1);
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
        Debug.Log("HandleCorrectInput() is called.");
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
        Debug.Log("HandleIncorrectInput() is called.");
        Ans.color = Color.red;
        Ans.text = "Invalid";
        StartCoroutine(ClearMessage(2f)); // Hide the invalid message after 2 seconds
    }

    private IEnumerator ClearMessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        Ans.color = Color.black;
        Ans.text = "";
    }

    private void ShowMessage(string message)
    {
        Debug.Log("ShowMessage('" + message + "') is called.");
        messageText.text = message;
        messageText.enabled = true;
        isMessageShown = true; // Set isMessageShown to true when showing the message
    }

    private void HideMessage()
    {
        Debug.Log("HideMessage() is called.");
        messageText.enabled = false;
    }
}
