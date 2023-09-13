using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; //singleton pattern 

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectsSource;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //never destroy this game object when load
        }
        else
        {
            Destroy(gameObject); //only 1 sound manager can be exist
        }
    }
    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
    public void ChangeEffectsVolume(float value)
    {
        _effectsSource.volume = value;
    }
    public void ChangeMusicVolume(float value)
    {
        _musicSource.volume = value;
    }
}
