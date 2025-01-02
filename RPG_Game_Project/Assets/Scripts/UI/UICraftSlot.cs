using UnityEngine.EventSystems;

public class UICraftSlot : UiItemSlot
{

    int DefaultNameFontSize = 25;


    protected override void Start()
    {
        base.Start();
    }


    public void setupcraftslot(ItemDataEquipment _item)
    {
        if (_item == null) return;

        Item.data = _item;

        ItemImage.sprite = _item.Icon;
        ItemText.text = _item.name;

        if(ItemText.text.Length > 12)
        {
            ItemText.fontSize = ItemText.fontSize * .7f;
        }
        else
        {
            ItemText.fontSize = DefaultNameFontSize;
        }

    }

    public override void OnPointerDown(PointerEventData eventData)
    {

        ui.CraftPanel.setupcraftpanel(Item.data as ItemDataEquipment);


    }
}
