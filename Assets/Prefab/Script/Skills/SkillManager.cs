using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillManager: MonoBehaviour
{
    private int selectedLevel; // Store the selected level ID

    private void Start()
    {
        // Retrieve the selected skill from PlayerPrefs
        int selectedSkill = PlayerPrefs.GetInt("SelectedSkill", 1); // Default to Skill ID 1 if not found

        // Enable the selected skill
        EnableSelectedSkill(selectedSkill);

        // Retrieve the selected level from PlayerPrefs
        selectedLevel = PlayerPrefs.GetInt("SelectedLevel", 1); // Default to Level ID 1 if not found
    }

    private void EnableSelectedSkill(int selectedSkill)
    {
        GameObject skill = GameObject.Find("Skill" + selectedSkill);
        if (skill != null)
        {
            skill.SetActive(true);
            Debug.Log("Skill " + selectedSkill + " enabled.");
        }
        else
        {
            Debug.LogError("Skill " + selectedSkill + " not found.");
        }
    }

    public void SelectSkill(int skillId)
    {
        StartCoroutine(SelectSkillCoroutine(skillId));
    }

    private System.Collections.IEnumerator SelectSkillCoroutine(int skillId)
    {
        for (int i = 1; i <= 4; i++) // Assuming you have 4 skills
        {
            if (i == skillId)
            {
                // Enable or perform actions for the selected skill
                EnableSelectedSkill(i);

                // Save the selected skill
                SaveSelectedSkill(i);
            }
        }

        // Yield for one frame to ensure that the log message appears after skill selection
        yield return null;

        // Log the selected skill
        Debug.Log("Selected Skill: " + skillId);
    }

    private void SaveSelectedSkill(int selectedSkill)
    {
        PlayerPrefs.SetInt("SelectedSkill", selectedSkill);
        PlayerPrefs.Save();
    }

    public void StartGame()
    {
        // Load the selected level using the selectedLevel variable
        string levelToLoad = "Level" + selectedLevel;
        SceneManager.LoadScene(levelToLoad);
    }
}
