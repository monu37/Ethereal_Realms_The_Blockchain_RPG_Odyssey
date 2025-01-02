using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] int TotalPossibleDrop;
    [SerializeField] ItemData[] PossibleDrop;
    List<ItemData> DroppedList = new List<ItemData>();

    [SerializeField] GameObject DropPrefab;


    public virtual void generatedrop()
    {
        for (int i = 0; i < PossibleDrop.Length; i++)
        {
            if(Random.Range(0,100) <= PossibleDrop[i].DropChance)
            {
                DroppedList.Add(PossibleDrop[i]);   
            }
        }

        for (int i = 0;i < TotalPossibleDrop; i++)
        {
            if(DroppedList.Count < 0)
            {
                return;
            }

            ItemData randomitem = DroppedList[Random.Range(0, DroppedList.Count - 1)];

            DroppedList.Remove(randomitem);
            dropitem(randomitem);
        }
    }

    protected void dropitem(ItemData _item)
    {
        GameObject newdrop = Instantiate(DropPrefab, transform.position, Quaternion.identity);

        Vector2 newvelocity = new Vector2(Random.Range(-5, 5), Random.Range(15, 20));

        newdrop.GetComponent<ItemObject>().setupitem(_item, newvelocity);
    }

    public void canceldrop()
    {
        TotalPossibleDrop = 0;
        print("Aaa");
    }
}
