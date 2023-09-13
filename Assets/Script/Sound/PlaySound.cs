using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    // This just an example to call sound playing in your own script 
    // Can make extra scripts to play your own sound according to 
    // your gameobject behavior or direct in existing scripts
    void Start()
    {
        SoundManager.Instance.PlaySound(_clip);
    }
}
