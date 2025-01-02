using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] Rigidbody2D Rb;

    [SerializeField] ItemData itemData;
   
    private void setupvisual()
    {
        if (itemData == null) return;

        GetComponent<SpriteRenderer>().sprite = itemData.Icon;

        gameObject.name = "Item Object: " + itemData.name;
    }

   

    public void setupitem(ItemData _itemdata, Vector2 _velocity)
    {
        itemData = _itemdata;

        Rb.velocity = _velocity;

        setupvisual();
    }

    public void pickupitem()
    {
        if (!Inventory.Instance.IsCanAddItems() || itemData.itemType == ItemType.Equipment) 
        {
            Rb.velocity = new Vector2(0, 7);
            PlayerManager.instance.player.Fx.createpopuptext("Inventory is full");
            return; 
        }


        AudioManager.instance.playsfx(18, transform);
        print("Picked Item: " + itemData.name);
        Inventory.Instance.additem(itemData);
        Destroy(gameObject);
    }
}
