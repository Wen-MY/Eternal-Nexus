using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject levelMenu;
    public GameObject skillSelectionPanel;
    public GameObject startGame;
    public int selectedLevel;

    private void Awake()
    {
        levelMenu.SetActive(true);
        skillSelectionPanel.SetActive(false);
        LoadSavedLevel(); // Load the previously selected level
    }

    public void SelectLevel(int lvlId)
    {
        Debug.Log("Selected level: " + lvlId);
        selectedLevel = lvlId;
        SaveSelectedLevel(); // Save the selected level
        levelMenu.SetActive(false);
        skillSelectionPanel.SetActive(true);
    }


    public void LoadLevel(int lvlId)
    {
        // Construct the scene name based on the selected level ID
        string lvlName = "Level" + lvlId;

        // Load the scene based on the constructed scene name
        SceneManager.LoadScene(lvlName);
    }

    private void SaveSelectedLevel()
    {
        PlayerPrefs.SetInt("SelectedLevel", selectedLevel);
        PlayerPrefs.Save();
    }

    private void LoadSavedLevel()
    {
        selectedLevel = PlayerPrefs.GetInt("SelectedLevel", 1); // Default to level 1 if not found
    }
}
