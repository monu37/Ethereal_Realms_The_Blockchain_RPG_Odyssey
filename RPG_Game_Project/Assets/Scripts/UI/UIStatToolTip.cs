using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIStatToolTip : UiToolTip
{
    [SerializeField] TextMeshProUGUI DescriptionText;

    public void showstattooltip(string _description)
    {
        DescriptionText.text = _description;
        adjusttooltipposition();

        gameObject.SetActive(true);
    }

    public void hidestattooltip()
    {
        DescriptionText.text = "";
        gameObject.SetActive(false);
    }
}
