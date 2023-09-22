using UnityEngine;

public class SkillManagerInLevel : MonoBehaviour
{
    public Flight flyScript;
    public PlayerTeleporterManager teleportScript;
    public Dashing dashScript;
    public GameObject shieldUI; 

    private void Start()
    {
        shieldUI = GameObject.Find("ShieldUI");
        int selectedSkill = PlayerPrefs.GetInt("SelectedSkill"); // Default to Skill ID 1 if not found

        // Enable or disable scripts based on the selected skill
        switch (selectedSkill)
        {
            case 1:
                flyScript.enabled = true;
                teleportScript.enabled = false;
                dashScript.enabled = false;
                shieldUI.SetActive(false); // Deactivate shieldUI for skill 1
                break;
            case 2:
                flyScript.enabled = false;
                teleportScript.enabled = true;
                dashScript.enabled = false;
                if (shieldUI != null)
                shieldUI.SetActive(false); // Deactivate shieldUI for skill 2
                break;
            case 3:
                flyScript.enabled = false;
                teleportScript.enabled = false;
                dashScript.enabled = true;
                shieldUI.SetActive(false); // Deactivate shieldUI for skill 3
                break;
            case 4:
                flyScript.enabled = false;
                teleportScript.enabled = false;
                dashScript.enabled = false;
                shieldUI.SetActive(true); // Activate shieldUI for skill 4
                
                break;
            default:
                // Handle invalid skill ID or other cases here
                break;
        }
    }
}
