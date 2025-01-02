using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISkillTooltip : UiToolTip
{
   [SerializeField] TextMeshProUGUI SkillNameText;
   [SerializeField] TextMeshProUGUI SkillDescriptionText;

    [SerializeField] TextMeshProUGUI SkillCostText;

    private void Start()
    {
      
    }

    public void showtooltip(string _skilldescription, string _skillname, int _skillcost)
    {
        SkillDescriptionText.text = _skilldescription;
        SkillNameText.text = _skillname;
        SkillCostText.text = "Cost :" + _skillcost.ToString();

        adjusttooltipposition();

        gameObject.SetActive(true);
    }

    public void hidetooltip()
    {
        
        gameObject.SetActive(false);
    }


}
