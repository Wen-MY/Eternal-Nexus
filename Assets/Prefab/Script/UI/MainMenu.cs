using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public AudioClip mainMenuBGM;
    private void Start()
    {
        SoundManager.Instance.ChangeBGM(mainMenuBGM);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit(); 
    }
}
