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

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        if (pauseMenu.activeInHierarchy)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerMovement.enabled = false;
            weaponSwitcher.enabled = false;
            inventoryManager.enabled = false;

        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            playerMovement.enabled = true;
            weaponSwitcher.enabled = true;
            inventoryManager.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

    }

    public void Pause()
    {
        darkPanel.SetActive(true);
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
    }
    public void Resume()
    {
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