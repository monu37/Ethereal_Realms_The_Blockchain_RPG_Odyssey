using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDrop : ItemDrop
{
    [Header("Player's Drop")]
    [Range(0, 100)]
    [SerializeField] float ChanceToLooseItems;
    [Range(0,100)]
    [SerializeField] float ChanceToLooseMaterials;

    public override void generatedrop()
    {
        //list if equipment
        Inventory inventory = Inventory.Instance;



        List<InventoryItem> currentequipment = inventory.GetEquipmentList();
        List<InventoryItem> itemstounequip = new List<InventoryItem>();


        List<InventoryItem> currentstash = inventory.GetStashList();
        List<InventoryItem> materialtoloose = new List<InventoryItem>();


        //foreach item we gonna check if should losse item

        foreach (InventoryItem item in currentequipment)
        {
            if (Random.Range(0, 100) <= ChanceToLooseItems)
            {
                dropitem(item.data);
                itemstounequip.Add(item);

            }
        }

        for (int i = 0; i < itemstounequip.Count; i++)
        {
            inventory.unequipitem(itemstounequip[i].data as ItemDataEquipment);
        }


        // for stash

        foreach (InventoryItem item in currentstash)
        {
            if (Random.Range(0, 100) <= ChanceToLooseMaterials)
            {
                dropitem(item.data);
                materialtoloose.Add(item);

            }
        }

        for(int i = 0;i < materialtoloose.Count;i++)
        {
            inventory.removeitem(materialtoloose[i].data); 
        }
    }
}
