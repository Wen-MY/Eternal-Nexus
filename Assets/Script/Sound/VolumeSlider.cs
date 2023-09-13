using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider _slider;
    public SoundType Control;

    public enum SoundType {
        Master,
        Music,
        Effects,
    }

    // Start is called before the first frame update
    void Start()
    {
        switch (Control) { 
        case SoundType.Music:
                SoundManager.Instance.ChangeMusicVolume(_slider.value);
                _slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMusicVolume(val));
                break;
        case SoundType.Effects:
                SoundManager.Instance.ChangeEffectsVolume(_slider.value);
                _slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeEffectsVolume(val));
                break;
        default:
                SoundManager.Instance.ChangeMasterVolume(_slider.value);
                _slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMasterVolume(val));
                break;
        }

        
    }

}
