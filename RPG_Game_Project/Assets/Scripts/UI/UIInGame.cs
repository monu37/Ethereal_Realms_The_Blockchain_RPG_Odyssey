using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    [SerializeField]PlayerStats playerStats;
    [SerializeField] Slider slider;

    [SerializeField] Image DashImage;
    [SerializeField] Image ParryImage;
    [SerializeField] Image CrystalImage;
    [SerializeField] Image SwordImage;
    [SerializeField] Image BlackHoleImage;
    [SerializeField] Image FlaskImage;

    [Header("Currency Info")]
    [SerializeField] TextMeshProUGUI CurrentCurrencyText;
    [SerializeField] float CurrencyAmount;
    [SerializeField] float IncreaseRate = 100;

    SkillManager skills;
    private void Start()
    {
        if(playerStats != null) 
        {
            playerStats.OnHealthChange += updatehealthui;
        }

        skills = SkillManager.instance;
    }
    private void Update()
    {
        updatecurrencyui();

        if (Input.GetKeyUp(KeyCode.LeftShift) && skills.Dash.IsDashUnlocked) //dash
        {
            setcooldown(DashImage);
        }
        if (Input.GetKeyUp(KeyCode.Q) && skills.Parry.IsParryUnlocked) //parry
        {
            setcooldown(ParryImage);
        }
        if (Input.GetKeyUp(KeyCode.F) && skills.Crystal.IsCrystalUnlocked) //crystal
        {
            setcooldown(CrystalImage);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && skills.Sword.IsSwordUnlocked) //sword
        {
            setcooldown(SwordImage);
        }

        if (Input.GetKeyUp(KeyCode.R) && skills.Blackhole.IsBlackHoleUnlocked) //black hole
        {
            setcooldown(BlackHoleImage);
        }


        if (Input.GetKeyUp(KeyCode.Alpha1) && Inventory.Instance.GetEquipment(EquipmentType.Flask) != null)  //flask
        {
            setcooldown(FlaskImage);
        }


        checkcooldown(DashImage, skills.Dash.DefaultCoolDown);
        checkcooldown(ParryImage, skills.Parry.DefaultCoolDown);
        checkcooldown(CrystalImage, skills.Crystal.DefaultCoolDown);
        checkcooldown(SwordImage, skills.Sword.DefaultCoolDown);
        checkcooldown(BlackHoleImage, skills.Blackhole.DefaultCoolDown);

        checkcooldown(FlaskImage, Inventory.Instance.FlaskCoolDown);
    }

    private void updatecurrencyui()
    {
        if (CurrencyAmount < PlayerManager.instance.GetCurrency())
        {
            CurrencyAmount += Time.deltaTime * IncreaseRate;
        }
        else
        {
            CurrencyAmount = PlayerManager.instance.GetCurrency();
        }

        CurrentCurrencyText.text = Mathf.RoundToInt(CurrencyAmount).ToString("#,#");
    }

    void updatehealthui()
    {
        slider.maxValue = playerStats.GetMaxHealthValue();
        slider.value = playerStats.CurrentHealth;
    }

    public void setcooldown(Image _image)
    {
        if(_image.fillAmount <= 0)
        {
            _image.fillAmount = 1;
        }
    }

    void checkcooldown(Image _image,float _cooldown)
    {
        if(_image.fillAmount > 0)
        {
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
        }
    }

}
