using System;

[Serializable]
public class InventoryItem
{
    public ItemData data;
    public int StackSize;

    public InventoryItem(ItemData _newitemdata)
    {
        data = _newitemdata;

        //add to stack
        addstack();
    }

    public void addstack() => StackSize++;

    public void removestack() => StackSize--;
}
