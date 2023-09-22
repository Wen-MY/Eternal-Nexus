using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject darkPanel;
    public PlayerMovement playerMovement;
    public WeaponSwitcher weaponSwitcher;
    public InventoryManager inventoryManager;

    private bool isPaused = false;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        weaponSwitcher = GameObject.Find("Gun Holder").GetComponent<WeaponSwitcher>();
    }

    void Update()
    {
        if (isPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            weaponSwitcher.enabled = false;
            inventoryManager.enabled = false;
            playerMovement.enabled = false; // Disable player movement when paused
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            weaponSwitcher.enabled = true;
            inventoryManager.enabled = true;
            playerMovement.enabled = true; // Enable player movement when not paused
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        darkPanel.SetActive(true);
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isPaused = false;
        darkPanel.SetActive(false);
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void Exit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
