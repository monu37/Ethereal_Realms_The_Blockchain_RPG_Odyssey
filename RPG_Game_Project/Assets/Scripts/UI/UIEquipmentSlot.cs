using UnityEngine.EventSystems;

public class UIEquipmentSlot : UiItemSlot
{
    public EquipmentType SlotType;

    private void OnValidate()
    {
        gameObject.name = "Equipment Slot : " + SlotType.ToString();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (Item == null || Item.data == null) return;

        //inventory unequip item
        Inventory.Instance.unequipitem(Item.data as ItemDataEquipment);
        Inventory.Instance.additem(Item.data as ItemDataEquipment);

        ui.ItemToolTip.hidetoolinfo();

        cleanupslot();

    }
}
