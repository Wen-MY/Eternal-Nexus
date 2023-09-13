using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public GameObject levelButtons;
    public GameObject skillSelectionPanel;
    private int selectedLevel;

    private void Awake()
    {
        levelButtons.SetActive(true);
        skillSelectionPanel.SetActive(false);
    }

    public void SelectLevel(int lvlId)
    {
        selectedLevel = lvlId;
        levelButtons.SetActive(false);
        skillSelectionPanel.SetActive(true);
    }

    private void LoadLevel(int lvlId)
    {
        // Construct the scene name based on the selected level ID
        string lvlName = "Level" + lvlId;

        // Load the scene based on the constructed scene name
        SceneManager.LoadScene(lvlName);
    }
}
