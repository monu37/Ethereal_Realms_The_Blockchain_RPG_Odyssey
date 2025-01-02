using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiCraftList : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] int CraftListNo;
    [SerializeField] Transform CraftSlotParent;
    [SerializeField] GameObject CraftSlotPrefab;

    [SerializeField] List<ItemDataEquipment> CraftEquipmentList;

    private void Start()
    {
        transform.parent.GetChild(0).GetComponent<UiCraftList>().setupcraftlist();
        setupdeafultcraftpanel();
    }



    public void setupcraftlist()
    {
        for (int i = 0; i < CraftSlotParent.childCount; i++)
        {
            Destroy(CraftSlotParent.GetChild(i).gameObject);
        }



        for (int i = 0; i < CraftEquipmentList.Count; i++)
        {
            GameObject newslot = Instantiate(CraftSlotPrefab, CraftSlotParent);
            newslot.GetComponent<UICraftSlot>().setupcraftslot(CraftEquipmentList[i]);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        setupcraftlist();
    }

    public void setupdeafultcraftpanel()
    {
        if (CraftEquipmentList[0] !=null && CraftListNo ==0)
        {
            GetComponentInParent<UI>().CraftPanel.setupcraftpanel(CraftEquipmentList[0]);  
        }
    }
}
