using System;
using UnityEngine;

[Serializable]
public class ItemInventory
{
    [SerializeField] protected int itemId;
    public int ItemID => itemId;

    [SerializeField] protected ItemCode itemCode;
    public ItemCode ItemCode => itemCode;

    protected ItemProfileSO itemProfile;
    public ItemProfileSO ItemProfile => itemProfile;
    
    [SerializeField] protected string itemName;
    
    public int itemCount;

    public ItemInventory(ItemProfileSO itemProfile, int itemCount)
    {
        this.itemProfile = itemProfile;
        this.itemCount = itemCount;
        if (this.itemProfile != null)
        {
            this.itemName = this.itemProfile.itemName;
            this.itemCode = itemProfile.itemCode;
        }
    }

    public virtual void RandomId()
    {
        this.itemId = UnityEngine.Random.Range(0, 999999999);
    }

    public virtual void SetName(string name)
    {
        this.itemName = name;
    }

    public virtual string GetItemName()
    {
        if (this.itemName == null || this.itemName == "") return this.itemProfile.itemName;
        return this.itemName;
    }

    public virtual bool Deduct(int number)
    {
        if (!this.CanDeduct(number)) return false;
        this.itemCount -= number;
        return true;
    }

    public virtual bool CanDeduct(int number)
    {
        if (this.itemProfile.canNegative) return true;
        return this.itemCount >= number;
    }

    public virtual string Info()
    {
        return $"{itemName}: {itemCount}";
    }
}
