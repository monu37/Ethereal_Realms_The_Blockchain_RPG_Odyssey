using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler,IPointerExitHandler
{

    [SerializeField]protected  Image ItemImage;
    [SerializeField] protected TextMeshProUGUI ItemText;

    public InventoryItem Item;

    protected UI ui;

    protected virtual void Start()
    {
        ui = GetComponentInParent<UI>();
    }
    public void updateslots(InventoryItem _item)
    {
        Item = _item;

        ItemImage.color = Color.white;


        if (Item != null)
        {
            ItemImage.sprite = Item.data.Icon;

            if (Item.StackSize > 1)
            {
                ItemText.text = Item.StackSize.ToString();
            }
            else
            {
                ItemText.text = "";
            }
        }
    }

    public void cleanupslot()
    {
        Item = null;

        ItemImage.sprite = null;
        ItemImage.color = Color.clear;
        ItemText.text = "";
    }


    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (Item == null) return;

        //REMOVE ITEM FROM INVENTORY
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Inventory.Instance.removeitem(Item.data);
            ui.ItemToolTip.hidetoolinfo();
            return;
        }

        if (Item.data.itemType == ItemType.Equipment)
        {
            Inventory.Instance.equipitem(Item.data);
        }

        ui.ItemToolTip.hidetoolinfo();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Item == null) return;

        print("Show item info");
       

        ui.ItemToolTip.showtoolinfo(Item.data as ItemDataEquipment);



    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(Item == null) return;

        print("Hide Item Info");
        ui.ItemToolTip.hidetoolinfo();

    }
}
