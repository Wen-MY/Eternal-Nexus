using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3SFX : MonoBehaviour
{
    public AudioSource soundPlayer;

    public void playThisSoundEffect()
    {
        soundPlayer.Play();
    }
}
