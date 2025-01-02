using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour, ISaveManager
{
    public static Inventory Instance;

    public List<ItemData> StartingItems;



    public List<InventoryItem> Equipment;
    public Dictionary<ItemDataEquipment, InventoryItem> EquipmentDictionary;

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> InventoryDictionary;

    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> StashDictionary;


    [Header("Inventory UI")]
    [SerializeField] Transform InventorySlotParent;
    [SerializeField] Transform StashSlotParent;
    [SerializeField] Transform EquipmentSlotParent;
    [SerializeField] Transform StatSlotParent;

    UIEquipmentSlot[] equipmentSlots;
    UiItemSlot[] InventoryItemSlots;
    UiItemSlot[] StashItemSlots;
    UIStatSlot[] StatSlots;

    [Header("Items CoolDown")]
    float FlaskLastTimeUsed;
    float LastTimeUsedArmor;

    public float FlaskCoolDown { get; private set; }
    float ArmorCoolDown;


    [Header("Data Base")]
    public List<InventoryItem> LoadedItems;
    public List<ItemDataEquipment> LoadedEquipment;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        inventory = new List<InventoryItem>();
        InventoryDictionary = new Dictionary<ItemData, InventoryItem>();

        stash = new List<InventoryItem>();
        StashDictionary = new Dictionary<ItemData, InventoryItem>();

        Equipment = new List<InventoryItem>();
        EquipmentDictionary = new Dictionary<ItemDataEquipment, InventoryItem>();



        InventoryItemSlots = InventorySlotParent.GetComponentsInChildren<UiItemSlot>();
        StashItemSlots = StashSlotParent.GetComponentsInChildren<UiItemSlot>();
        equipmentSlots = EquipmentSlotParent.GetComponentsInChildren<UIEquipmentSlot>();
        StatSlots = StatSlotParent.GetComponentsInChildren<UIStatSlot>();


        //
        addstartingitems();

    }

    private void addstartingitems()
    {

        foreach (ItemDataEquipment item in LoadedEquipment)
        {
            equipitem(item);
        }

        if (LoadedItems.Count > 0)
        {
            print("Get data from last save ");

            foreach (InventoryItem item in LoadedItems)
            {
                for (int i = 0; i < item.StackSize; i++)
                {
                    additem(item.data);
                }
            }

            return;
        }


        for (int i = 0; i < StartingItems.Count; i++)
        {
            if (StartingItems[i] != null)
            {
                additem(StartingItems[i]);
            }
        }


    }

    public void equipitem(ItemData _item)
    {
        ItemDataEquipment neweqipment = _item as ItemDataEquipment;
        InventoryItem newitem = new InventoryItem(neweqipment);


        ItemDataEquipment oldequipment = null;

        foreach (KeyValuePair<ItemDataEquipment, InventoryItem> item in EquipmentDictionary)
        {
            if (item.Key.equipmentType == neweqipment.equipmentType)
            {
                oldequipment = item.Key;
            }
        }

        if (oldequipment != null)
        {
            unequipitem(oldequipment);
            additem(oldequipment);

        }

        Equipment.Add(newitem);
        EquipmentDictionary.Add(neweqipment, newitem);

        neweqipment.addmodifers();

        removeitem(_item);

        updateslotui();
        //

    }

    public void unequipitem(ItemDataEquipment _itemtoremove)
    {
        if (EquipmentDictionary.TryGetValue(_itemtoremove, out InventoryItem _value))
        {
            Equipment.Remove(_value);
            EquipmentDictionary.Remove(_itemtoremove);
            _itemtoremove.removemodifers();
        }
    }

    void updateslotui()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            foreach (KeyValuePair<ItemDataEquipment, InventoryItem> item in EquipmentDictionary)
            {
                if (item.Key.equipmentType == equipmentSlots[i].SlotType)
                {
                    equipmentSlots[i].updateslots(item.Value);
                }
            }
        }


        for (int i = 0; i < InventoryItemSlots.Length; i++)
        {
            InventoryItemSlots[i].cleanupslot();
        }

        for (int i = 0; i < StashItemSlots.Length; i++)
        {
            StashItemSlots[i].cleanupslot();
        }



        for (int i = 0; i < inventory.Count; i++)
        {
            InventoryItemSlots[i].updateslots(inventory[i]);
        }

        for (int i = 0; i < stash.Count; i++)
        {
            StashItemSlots[i].updateslots(stash[i]);
        }

        updatestatsui();
    }

    public void updatestatsui()
    {
        //update info of stats in character UI
        for (int i = 0; i < StatSlots.Length; i++)
        {
            StatSlots[i].updatestatvalueui();
        }
    }

    public void additem(ItemData _item)
    {

        if (_item.itemType == ItemType.Equipment && IsCanAddItems())
        {
            addtoinventory(_item);
        }

        else if (_item.itemType == ItemType.Material)
        {
            addtostash(_item);
        }



        updateslotui();
    }

    private void addtostash(ItemData _item)
    {
        if (StashDictionary.TryGetValue(_item, out InventoryItem _value))
        {
            _value.addstack();
        }
        else
        {
            InventoryItem newitem = new InventoryItem(_item);
            stash.Add(newitem);

            StashDictionary.Add(_item, newitem);
        }
    }

    private void addtoinventory(ItemData _item)
    {
        if (InventoryDictionary.TryGetValue(_item, out InventoryItem _value))
        {
            _value.addstack();
        }
        else
        {
            InventoryItem newitem = new InventoryItem(_item);
            inventory.Add(newitem);

            InventoryDictionary.Add(_item, newitem);

        }
    }

    public void removeitem(ItemData _item)
    {
        if (InventoryDictionary.TryGetValue(_item, out InventoryItem _value))
        {
            if (_value.StackSize <= 1)
            {

                inventory.Remove(_value);
                InventoryDictionary.Remove(_item);
            }
            else
            {
                _value.removestack();
            }
        }

        if (StashDictionary.TryGetValue(_item, out InventoryItem _stashvalue))
        {
            if (_stashvalue.StackSize <= 1)
            {
                stash.Remove(_stashvalue);
                StashDictionary.Remove(_item);
            }
            else
            {
                _stashvalue.removestack();
            }
        }

        updateslotui();
    }

    public bool IsCanAddItems()
    {
        if (inventory.Count >= InventoryItemSlots.Length)
        {
            print("No More Space In Inventory");
            return false;
        }
        return true;
    }

    public bool IsCanCraft(ItemDataEquipment _itemtocraft, List<InventoryItem> _requiredmat)
    {
        List<InventoryItem> materialstoremove = new List<InventoryItem>();

        for (int i = 0; i < _requiredmat.Count; i++)
        {
            if (StashDictionary.TryGetValue(_requiredmat[i].data, out InventoryItem _stashvalue))
            {

                // add this to used mat
                if (_stashvalue.StackSize < _requiredmat[i].StackSize)
                {
                    print("Not enough materials");
                    return false;

                }
                else
                {
                    materialstoremove.Add(_stashvalue);
                }

            }
            else
            {
                print("Not enough materials");
                return false;
            }
        }

        for (int i = 0; i < materialstoremove.Count; i++)
        {
            removeitem(materialstoremove[i].data);
        }

        additem(_itemtocraft);
        print("Here is your item: " + _itemtocraft.name);
        return true;
    }

    public List<InventoryItem> GetEquipmentList() => Equipment;

    public List<InventoryItem> GetStashList() => stash;

    public ItemDataEquipment GetEquipment(EquipmentType _type)
    {
        ItemDataEquipment quipitem = null;

        foreach (KeyValuePair<ItemDataEquipment, InventoryItem> item in EquipmentDictionary)
        {
            if (item.Key.equipmentType == _type)
            {
                quipitem = item.Key;
            }
        }

        return quipitem;
    }

    public void useflask()
    {
        //can use cooldown

        //use flask
        //set cooldown

        ItemDataEquipment currentflask = GetEquipment(EquipmentType.Flask);

        if (currentflask == null) return;

        bool IsCanUseFlask = Time.time > FlaskLastTimeUsed + FlaskCoolDown;

        if (IsCanUseFlask)
        {

            FlaskCoolDown = currentflask.ItemCoolDown;
            print("Used Flask");

            currentflask.effect(null);
            FlaskLastTimeUsed = Time.time;
        }
        else
        {
            print("Flask on CoolDown");
        }


    }

    public bool IsCanUseArmor()
    {
        ItemDataEquipment currentarmor = Inventory.Instance.GetEquipment(EquipmentType.Armor);


        if (Time.time > LastTimeUsedArmor + ArmorCoolDown)
        {

            ArmorCoolDown = currentarmor.ItemCoolDown;
            LastTimeUsedArmor = Time.time;

            return true;
        }

        print("Armor on CoolDown");
        return false;
    }

    public void loaddata(GameData _data)
    {

        foreach (KeyValuePair<string, int> pair in _data.inventory)
        {
            foreach (var item in GetItemDataBase())
            {
                if (item != null && item.ItemId == pair.Key)
                {
                    InventoryItem itemtoload = new InventoryItem(item);
                    itemtoload.StackSize = pair.Value;

                    LoadedItems.Add(itemtoload);
                }
            }
        }

        foreach (string loadeditemid in _data.EquipmentId)
        {
            foreach (var item in GetItemDataBase())
            {
                if (item != null && loadeditemid == item.ItemId)
                {
                    LoadedEquipment.Add(item as ItemDataEquipment);
                }
            }
        }
        print("Inventory Items Loaded");
    }

    public void savedata(ref GameData _data)
    {
        _data.inventory.Clear();
        _data.EquipmentId.Clear();
        print("save inventory");

        //inventory
        foreach (KeyValuePair<ItemData, InventoryItem> pair in InventoryDictionary)
        {
            _data.inventory.Add(pair.Key.ItemId, pair.Value.StackSize);
        }

        //stash
        foreach (KeyValuePair<ItemData, InventoryItem> pair in StashDictionary)
        {
            _data.inventory.Add(pair.Key.ItemId, pair.Value.StackSize);
        }

        //equipment
        foreach (KeyValuePair<ItemDataEquipment, InventoryItem> pair in EquipmentDictionary)
        {
            _data.EquipmentId.Add(pair.Key.ItemId);
        }
    }

    List<ItemData> GetItemDataBase()
    {
        List<ItemData> ItemDataBase = new List<ItemData>();

#if UNITY_EDITOR
        string[] AssetsNames = AssetDatabase.FindAssets("", new[] { "Assets/Data/Items" });

        foreach (string SOName in AssetsNames)
        {
            var SOPath = AssetDatabase.GUIDToAssetPath(SOName);
            var itemdata = AssetDatabase.LoadAssetAtPath<ItemData>(SOPath);

            ItemDataBase.Add(itemdata);
        }
#endif

        return ItemDataBase;
    }
}
