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
    public FiringSystem firingSystem;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        weaponSwitcher = GameObject.Find("Gun Holder").GetComponent<WeaponSwitcher>();
    }

    void Update()
    {

        if (pauseMenu.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            weaponSwitcher.enabled = false;
            inventoryManager.enabled = false;



        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
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
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }
}