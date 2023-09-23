using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject darkPanel;
    public GameObject playerMechanism;
    public WeaponSwitcher weaponSwitcher;
    public InventoryManager inventoryManager;
    public FiringSystem firingSystem;
    public Shield shield;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        weaponSwitcher = GameObject.Find("Gun Holder").GetComponent<WeaponSwitcher>();
        firingSystem = GameObject.Find("Mechanism").GetComponent<FiringSystem>();
        shield = GameObject.Find("Skill Canvas/ShieldUI").GetComponent<Shield>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }

        firingSystem = GameObject.Find("Mechanism").GetComponent<FiringSystem>();

        if (pauseMenu.activeSelf)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerMechanism.SetActive(false);
            weaponSwitcher.enabled = false;
            inventoryManager.enabled = false;
            firingSystem.enabled = false;
            shield.enabled = false;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            playerMechanism.SetActive(true);
            weaponSwitcher.enabled = true;
            inventoryManager.enabled = true;
            firingSystem.enabled = true;
            shield.enabled= true;
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