using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour
{
    public GameObject levelMenu;
    public GameObject skillSelectionPanel;
    public GameObject startGame;
    public int selectedLevel;

    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private Image progressBar;
    private float target;
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

    /*
     * original loadlevel without load screen
    public void LoadLevel(int lvlId)
    {
        // Construct the scene name based on the selected level ID
        string lvlName = "Level" + lvlId;

        // Load the scene based on the constructed scene name
        SceneManager.LoadScene(lvlName);
    }
    */

    public async void LoadLevel(int lvlId)
    {

        skillSelectionPanel.SetActive(false);
        // Construct the scene name based on the selected level ID
        string lvlName = "Level" + lvlId;

        //target = 0;
        progressBar.fillAmount = 0;
        // Load the scene based on the constructed scene name
        var scene = SceneManager.LoadSceneAsync(lvlName);
        scene.allowSceneActivation = false;
        loadingCanvas.SetActive(true);
        do
        {
            await Task.Delay(100);
            target = scene.progress;
        } while (scene.progress < 0.9f);

        await Task.Delay(2000);
        scene.allowSceneActivation = true;
        loadingCanvas.SetActive(false);
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

    public void updateProgressBar()
    {
        progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, target, 3 * Time.deltaTime);
    }


    
}
