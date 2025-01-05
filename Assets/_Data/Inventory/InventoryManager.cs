using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : SaiSingleton<InventoryManager>
{
    [SerializeField] protected float karmaRate = 80;
    [SerializeField] protected float karmaDefaultRate = 80;
    [SerializeField] protected ItemInventory choosedItem;
    public ItemInventory ChoosedItem => choosedItem;

    [SerializeField] protected List<InventoryCtrl> inventories;
    [SerializeField] protected List<UIImages> itemProfiles;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadInventories();
        this.LoadItemProfiles();
    }
    protected virtual void LoadInventories()
    {
        if (this.inventories.Count > 0) return;
        this.CreateItemsInventory();
        this.CreateCurrencyInventory();


        foreach (Transform child in transform)
        {
            InventoryCtrl inventoryCtrl = child.GetComponent<InventoryCtrl>();
            if (inventoryCtrl == null) continue;
            this.inventories.Add(inventoryCtrl);
        }
        Debug.Log(transform.name + ": LoadInventories", gameObject);
    }

    protected virtual void CreateItemsInventory()
    {
        Transform items = this.CreateDefaultInventory(InvCodeName.Items.ToString());
        items.AddComponent<InventoryItems>();
    }

    protected virtual void CreateCurrencyInventory()
    {
        Transform items = this.CreateDefaultInventory(InvCodeName.Currency.ToString());
        items.AddComponent<InventoryMonies>();
    }

    protected virtual Transform CreateDefaultInventory(string name)
    {
        Transform objItems = transform.Find(name);
        if (objItems == null)
        {
            objItems = new GameObject(name).transform;
            objItems.parent = transform;
        }
        return objItems;
    }

    public virtual InventoryCtrl GetByCodeName(InvCodeName inventoryName)
    {
        foreach (InventoryCtrl inventory in this.inventories)
        {
            if (inventory.GetName() == inventoryName) return inventory;
        }

        return null;
    }

    public virtual UIImages GetProfileByCode(ItemCode itemCodeName)
    {
        foreach (UIImages itemProfile in this.itemProfiles)
        {
            if (itemProfile.itemCode == itemCodeName) return itemProfile;
        }

        return null;
    }

    public virtual InventoryCtrl Monies()
    {
        return this.GetByCodeName(InvCodeName.Currency);
    }

    public virtual InventoryCtrl Items()
    {
        return this.GetByCodeName(InvCodeName.Items);
    }

    public virtual void AddItem(ItemInventory itemInventory)
    {
        InvCodeName invCodeName = itemInventory.ItemProfile.invCodeName;
        InventoryCtrl inventoryCtrl = InventoryManager.Instance.GetByCodeName(invCodeName);
        inventoryCtrl.AddItem(itemInventory);
    }

    public virtual void AddItem(ItemCode itemCode, int itemCount)
    {
        UIImages itemProfile = InventoryManager.Instance.GetProfileByCode(itemCode);
        ItemInventory item = new(itemProfile, itemCount);
        this.AddItem(item);
    }

    public virtual void RemoveItem(ItemCode itemCode, int itemCount)
    {
        UIImages itemProfile = InventoryManager.Instance.GetProfileByCode(itemCode);
        ItemInventory item = new(itemProfile, itemCount);
        this.RemoveItem(item);
    }

    public virtual void RemoveItem(ItemInventory itemInventory)
    {
        InvCodeName invCodeName = itemInventory.ItemProfile.invCodeName;
        InventoryCtrl inventoryCtrl = InventoryManager.Instance.GetByCodeName(invCodeName);
        inventoryCtrl.RemoveItem(itemInventory);
    }

    protected virtual void LoadItemProfiles()
    {
        if (this.itemProfiles.Count > 0) return;
        UIImages[] itemProfileSOs = Resources.LoadAll<UIImages>("/");
        this.itemProfiles = new List<UIImages>(itemProfileSOs);
        Debug.Log(transform.name + ": LoadItemProfiles", gameObject);
    }

    public virtual ItemCode RandomItem()
    {
        int playerLevel = PlayerCtrl.Instance.Level.CurrentLevel;
        int karmaPercent = (int)(this.karmaDefaultRate - (playerLevel * 2.5));
        this.karmaRate = karmaPercent;
        int rand = Random.Range(0, 100);
        if (rand < this.karmaRate) return this.RandomKarma();
        return this.RandomFate();
    }

    public virtual ItemCode RandomKarma()
    {
        List<ItemCode> specificItems = new()
        {
            ItemCode.Meat,
            ItemCode.Gold,
        };
        int randomIndex = Random.Range(0, specificItems.Count);
        return specificItems[randomIndex];
    }

    public virtual ItemCode RandomFate()
    {
        List<ItemCode> specificItems = new()
        {
            ItemCode.Water,
            ItemCode.Banana,
        };
        int randomIndex = Random.Range(0, specificItems.Count);
        return specificItems[randomIndex];
    }

    public virtual void SetChoosedItem(ItemInventory itemInventory)
    {
        this.choosedItem = itemInventory;
    }

    public virtual void UseChoosedItem()
    {
        this.choosedItem.Deduct(1);
        if (this.choosedItem.ItemProfile.isKarma) this.AddItem(ItemCode.Karma, 1);
        else this.AddItem(ItemCode.Fate, 1);
    }
}
