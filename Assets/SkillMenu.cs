using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkillMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLvl(int lvlId)
    {
        string lvlName = "Level " + lvlId;
        SceneManager.LoadScene(lvlName);
    }
}
