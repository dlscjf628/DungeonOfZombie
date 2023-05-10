using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{

    public AudioMixer mixer;
    public Slider slider;
    public Slider slider2;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("BackGround", 0.7f);
        slider2.value = PlayerPrefs.GetFloat("Effect", 0.7f);
    }

    public void BackGroundSound()
    {
        float sliderValue = slider.value;
        mixer.SetFloat("BackGround", sliderValue);
        PlayerPrefs.SetFloat("BackGround", sliderValue);
    }

    public void EffectSound()
    {
        float sliderValue = slider2.value;
        mixer.SetFloat("Effect", sliderValue);
        PlayerPrefs.SetFloat("Effect", sliderValue);
    }
}
