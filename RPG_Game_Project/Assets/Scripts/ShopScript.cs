using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public static ShopScript instance;

    public bool IsShop;

    private void Awake()
    {
        instance = this;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.name ==("Player"))
        {
            print("Shop");
            IsShop = true;
            string shoptext = "Shop : Press Left Shift + P";
            GameManager.instance.setconversationtext(shoptext);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == ("Player"))
        {
            print("Shop exit");
            IsShop = false;
        }
    }
}
