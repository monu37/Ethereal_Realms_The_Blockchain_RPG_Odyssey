using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UiVolumeSlider : MonoBehaviour
{
     public Slider slider;
    public string Parameter;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] float Multiplier;



    public void slidervalue(float _value) => audioMixer.SetFloat(Parameter, Mathf.Log10(_value) * Multiplier);

    public void loadslider(float _value)
    {
        if(_value >= 0.001f)
        {
            slider.value = _value;
        }
    }
   
}
