using UnityEngine;

public class SkillMenu : MonoBehaviour
{
    private void Start()
    {
        // Retrieve the selected skill from PlayerPrefs
        int selectedSkill = PlayerPrefs.GetInt("SelectedSkill", 1); // Default to Skill ID 1 if not found

        // Enable the selected skill
        EnableSelectedSkill(selectedSkill);
    }

    private void EnableSelectedSkill(int selectedSkill)
    {
        // Enable the GameObject representing the selected skill
        GameObject skill = GameObject.Find("Skill" + selectedSkill);
        if (skill != null)
        {
            skill.SetActive(true);
        }
    }

    private void DisableSkill(int skillId)
    {
        GameObject skill = GameObject.Find("Skill" + skillId);
        if (skill != null)
        {
            skill.SetActive(false);
        }
    }

    public void SelectSkill(int skillId)
    {
        for (int i = 1; i <= 4; i++) // Assuming you have 4 skills
        {
            DisableSkill(i);

            if (i == skillId)
            {
                // Enable or perform actions for the selected skill
                EnableSelectedSkill(i);
            }
        }
    }
}
