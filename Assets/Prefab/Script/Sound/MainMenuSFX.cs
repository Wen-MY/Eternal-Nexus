using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSFX : MonoBehaviour
{
    public AudioSource soundPlayer;

    public void playThisSoundEffect()
    {
        soundPlayer.Play();
    }
}
