using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{

    
    public Button[] buttons;
    public GameObject levelButtons;
    private void Awake(){
        ButtonsToArray();

        //Lock and Unlock level(We dont implement in testing phase)
        /**int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1); //open the first level
        for (int i=0; i < buttons.Length; i++)
           {
                buttons[i].interactable = false;
            }
            for (int i = 0; i < unlockedLevel; i++)
            {
                buttons[i].interactable = true;
            } **/

    }

    public void OpenLvl(int lvlId)
    {
        string lvlName = "Level " + lvlId;
        SceneManager.LoadScene(lvlName);
    }

    private void ButtonsToArray()
    {
        int childCount = levelButtons.transform.childCount;
        buttons = new Button[childCount];
        for(int i = 0; i<childCount; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }
}
