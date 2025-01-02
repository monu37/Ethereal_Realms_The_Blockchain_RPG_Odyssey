using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiCraftPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ItemNameText;
    [SerializeField] TextMeshProUGUI ItemDescriptionText;
    [SerializeField] Image ItemIcon;
    [SerializeField] Button CraftBtn;

    [SerializeField] Image[] MaterialImages;

    public void setupcraftpanel(ItemDataEquipment _data)
    {
        CraftBtn.onClick.RemoveAllListeners();

        for (int i = 0; i < MaterialImages.Length; i++)
        {
            MaterialImages[i].color = Color.clear;
            MaterialImages[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
        }

        for (int i = 0; i < _data.CraftingMaterials.Count; i++)
        {
            if(_data.CraftingMaterials.Count > MaterialImages.Length)
            {
                Debug.LogWarning("You have more materials amount than you have material slots in craft panel");
            }

            MaterialImages[i].sprite = _data.CraftingMaterials[i].data.Icon;
            MaterialImages[i].color = Color.white;

            TextMeshProUGUI materialslottext= MaterialImages[i].GetComponentInChildren<TextMeshProUGUI>();

            materialslottext.text = _data.CraftingMaterials[i].StackSize.ToString();
            materialslottext.color = Color.white;


        }

        ItemIcon.sprite = _data.Icon;
        ItemNameText.text = _data.ItemName;
        ItemDescriptionText.text=_data.GetDescription();    


        CraftBtn.onClick.AddListener(()=> Inventory.Instance.IsCanCraft(_data, _data.CraftingMaterials));
    }


}
