using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectTrigger : MonoBehaviour
{
    ItemObject MyItemObject => GetComponentInParent<ItemObject>();

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>() != null)
        {

            if (col.GetComponent<CharacterStats>().IsDead) return;

            MyItemObject.pickupitem();
        }
    }
}
