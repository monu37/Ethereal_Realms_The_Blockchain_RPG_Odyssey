using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiItemToolInfo : UiToolTip
{
    [SerializeField] TextMeshProUGUI ItemNameText;
    [SerializeField] TextMeshProUGUI ItemTypeText;
    [SerializeField] TextMeshProUGUI ItemDescriptionText;

    private void Awake()
    {
        //ItemNameText=transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        //ItemTypeText=transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        //ItemDescriptionText=transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    public void showtoolinfo(ItemDataEquipment _item)
    {
        if(_item == null) return;

        ItemNameText.text = _item.ItemName;
        ItemTypeText.text = _item.equipmentType.ToString();
        ItemDescriptionText.text = _item.GetDescription();

        if (ItemNameText.text.Length > 12)
        {
            ItemNameText.fontSize = ItemNameText.fontSize * .7f;
        }
        else
        {
            ItemNameText.fontSize = 35;
        }

        adjusttooltipposition();

        gameObject.SetActive(true);


    }

    public void hidetoolinfo()
    {
        ItemNameText.fontSize = 35;
        gameObject.SetActive(false); 
    }
}
