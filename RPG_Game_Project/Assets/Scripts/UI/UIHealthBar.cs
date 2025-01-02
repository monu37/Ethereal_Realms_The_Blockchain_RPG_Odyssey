using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    Entity entity;
    RectTransform Rt;

    Slider slider;

    CharacterStats MyStats;

    private void Start()
    {
        entity = GetComponentInParent<Entity>();
        Rt = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();
        MyStats= GetComponentInParent<CharacterStats>();    


        updatehealthui();

        entity.OnFlipped += flipui;
        MyStats.OnHealthChange += updatehealthui;

    }

   
    void updatehealthui()
    {
        slider.maxValue = MyStats.GetMaxHealthValue();
        slider.value = MyStats.CurrentHealth;
    }






    private void OnDisable()
    {
        entity.OnFlipped -= flipui;
        MyStats.OnHealthChange -= updatehealthui;
    }

    void flipui()=> Rt.Rotate(0, 180, 0);



}
