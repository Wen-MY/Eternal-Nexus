using System.Collections;
using TMPro;
using UnityEngine;

public class Keypad : MonoBehaviour
{

    public TextMeshProUGUI Ans;
    public GameObject passwordPanel;
    public GameObject inventoryCanvas;
    public GameObject crosshair;
    public GameObject playerMechanism;
    public GameObject pauseMenu;

    private string Answer = "543210";
    private string enteredCode = "";
    private bool codeEntered = false;

    private void Start()
    {
        CloseKeyPad();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.O))
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

    public void ToggleKeyPad()
    {
        passwordPanel.SetActive(true);
        inventoryCanvas.SetActive(false);
        crosshair.SetActive(false);
        playerMechanism.SetActive(false);
        Time.timeScale = 0;

    }

    public void CloseKeyPad()
    {
        passwordPanel.SetActive(false);
        inventoryCanvas.SetActive(true);
        crosshair.SetActive(true);
        playerMechanism.SetActive(true);
        Time.timeScale = 1;
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

    private void HandleCorrectInput()
    {
        Ans.color = Color.green;
        Ans.text = "Correct";
        StartCoroutine(ClearEnteredCode(2f));
        StartCoroutine(CloseKeyPadWithDelay(2f));
    }

    private IEnumerator CloseKeyPadWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CloseKeyPad();
    }

    private void HandleIncorrectInput()
    {
        ToggleKeyPad();
        Ans.color = Color.red;
        Ans.text = "Invalid";
        StartCoroutine(ClearEnteredCode(2f));
    }

    private IEnumerator ClearEnteredCode(float delay)
    {
        yield return new WaitForSeconds(delay);
        Ans.color = Color.black;
        Ans.text = "";
        enteredCode = "";
        codeEntered = false;
    }
}
