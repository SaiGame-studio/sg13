using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class InventoryCtrl : SaiBehaviour
{
    [SerializeField] protected List<ItemInventory> items = new();
    public List<ItemInventory> Items => items;

    public abstract InvCodeName GetName();

    public virtual ItemInventory AddItem(ItemInventory item)
    {
        ItemInventory itemExist = this.FindItem(item.ItemProfile.itemCode);
        if (!item.ItemProfile.isStackable || itemExist == null)
        {
            item.RandomId();
            this.items.Add(item);
            return item;
        }

        itemExist.itemCount += item.itemCount;
        return itemExist;
    }

    public virtual bool RemoveItem(ItemInventory item)
    {
        int remoteCount = item.itemCount;
        ItemInventory itemExist = this.FindItemNotEmpty(item.ItemProfile.itemCode);
        if (itemExist == null)
        {
            item.RandomId();
            itemExist = item;
            itemExist.itemCount = 0;
            this.items.Add(itemExist);
        }
        if (!itemExist.CanDeduct(remoteCount)) return false;

        itemExist.Deduct(remoteCount);

        if (!itemExist.ItemProfile.canNegative && itemExist.itemCount == 0) this.items.Remove(itemExist);
        return true;
    }

    public virtual ItemInventory FindItem(ItemCode itemCode)
    {
        foreach (ItemInventory itemInventory in this.items)
        {
            if (itemInventory.ItemProfile.itemCode == itemCode) return itemInventory;
        }

        return null;
    }

    public virtual ItemInventory GetItem(ItemCode itemCode)
    {
        return this.FindItem(itemCode);
    }

    public virtual ItemInventory FindItemNotEmpty(ItemCode itemCode)
    {
        foreach (ItemInventory itemInventory in this.items)
        {
            if (itemInventory.ItemProfile.itemCode != itemCode) continue;
            if (itemInventory.itemCount > 0 || itemInventory.ItemProfile.canNegative) return itemInventory;
        }

        return null;
    }
}
