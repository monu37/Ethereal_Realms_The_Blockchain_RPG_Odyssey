using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIStatSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    UI ui;

    [SerializeField] bool IsIgnoreNameByType;
    [SerializeField] StatType Type;

    string StatName;

    TextMeshProUGUI StatValueText;
    TextMeshProUGUI StatNameText;

    [TextArea]
    [SerializeField] string StatDescription;

    private void OnValidate()
    {
        StatValueText = GetComponent<TextMeshProUGUI>();
        StatNameText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        StatName = Type.ToString();

        gameObject.name = "Stat : " + StatName;


        if (StatName != null && !IsIgnoreNameByType)
        {
            StatNameText.text = StatName;
        }
    }

    private void Start()
    {
        updatestatvalueui();

        ui = GetComponentInParent<UI>();
    }

    public void updatestatvalueui()
    {
        PlayerStats _playerstats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        if (_playerstats != null)
        {
            StatValueText.text = _playerstats.GetStat(Type).GetValue().ToString();

            //
            if (Type == StatType.Health)
            {
                StatValueText.text = _playerstats.GetMaxHealthValue().ToString();
            }

            if (Type == StatType.Damage)
            {
                StatValueText.text = _playerstats.Damage.GetValue() + _playerstats.Strength.GetValue().ToString();
            }

            if (Type == StatType.CritPower)
            {
                StatValueText.text = _playerstats.CritPower.GetValue() + _playerstats.Strength.GetValue().ToString();
            }

            if (Type == StatType.CritChance)
            {
                StatValueText.text = _playerstats.CritChance.GetValue() + _playerstats.Agility.GetValue().ToString();
            }

            if (Type == StatType.Evasion)
            {
                StatValueText.text = _playerstats.Evasion.GetValue() + _playerstats.Agility.GetValue().ToString();
            }

            if (Type == StatType.MagicRes)
            {
                StatValueText.text = _playerstats.MagicResistance.GetValue() + (_playerstats.Intelligence.GetValue() * 3).ToString();
            }

            if (Type == StatType.MagicRes)
            {
                StatValueText.text = _playerstats.MagicResistance.GetValue() + (_playerstats.Intelligence.GetValue() * 3).ToString();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {



        ui.StatToolTip.showstattooltip(StatDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.StatToolTip.hidestattooltip();//
    }
}
