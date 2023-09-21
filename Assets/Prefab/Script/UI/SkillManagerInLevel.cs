using UnityEngine;

public class SkillManagerInLevel : MonoBehaviour
{
    public Flight flyScript;
    public PlayerTeleporterManager teleportScript;
    public Dashing dashScript;
    public Shield shieldScript;

    private void Start()
    {
        int selectedSkill = PlayerPrefs.GetInt("SelectedSkill", 1); // Default to Skill ID 1 if not found

        // Enable or disable scripts based on the selected skill
        switch (selectedSkill)
        {
            case 1:
                flyScript.enabled = true;
                teleportScript.enabled = false;
                dashScript.enabled = false;
                shieldScript.enabled = false;
                break;
            case 2:
                flyScript.enabled = false;
                teleportScript.enabled = true;
                dashScript.enabled = false;
                shieldScript.enabled = false;
                break;
            case 3:
                flyScript.enabled = false;
                teleportScript.enabled = false;
                dashScript.enabled = true;
                shieldScript.enabled = false;
                break;
            case 4:
                flyScript.enabled = false;
                teleportScript.enabled = false;
                dashScript.enabled = false;
                shieldScript.enabled = true;
                break;
            default:
                // Handle invalid skill ID or other cases here
                break;
        }
    }
}
