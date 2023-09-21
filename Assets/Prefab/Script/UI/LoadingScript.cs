
using UnityEngine;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    [SerializeField]private LevelManager levelManager;
    // Update is called once per frame
    void Update()
    {
       levelManager.updateProgressBar();
    }
}
