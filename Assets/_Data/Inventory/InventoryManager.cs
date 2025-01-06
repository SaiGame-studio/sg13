using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : SaiSingleton<InventoryManager>
{
    [Header("Inventory")]
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

    public virtual void DeductItem(ItemCode itemCode, int itemCount)
    {
        this.RemoveItem(itemCode, itemCount);
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

    public virtual ItemCode RandomItem(List<ItemCode> specificItems)
    {
        int randomIndex = Random.Range(0, specificItems.Count);
        return specificItems[randomIndex];
    }

    public virtual void SetChoosedItem(ItemInventory itemInventory)
    {
        this.choosedItem = itemInventory;
    }

    public virtual void UseChoosedItem()
    {
        if (!this.choosedItem.Deduct(1)) return;
        int itemHunger = this.choosedItem.ItemProfile.hunger;
        int itemThirst = this.choosedItem.ItemProfile.thirst;

        if (itemHunger > 0)
        {
            if (!PlayerNeedsManager.Instance.CanEat(itemHunger)) return;
            else PlayerNeedsManager.Instance.Eat(itemHunger);
        }
        
        if (itemThirst > 0)
        {
            if (!PlayerNeedsManager.Instance.CanDrink(itemThirst)) return;
            else PlayerNeedsManager.Instance.Drink(itemThirst);
        }

        int fate = this.choosedItem.ItemProfile.fate;
        if (this.choosedItem.ItemProfile.isKarma) this.DeductFate(fate);
        else this.AddItem(ItemCode.Fate, fate);


    }

    public virtual void DeductFate(int deductNumber)
    {
        this.DeductItem(ItemCode.Fate, deductNumber);
        PlayerCtrl.Instance.Level.SetLevel(1);
    }
}
