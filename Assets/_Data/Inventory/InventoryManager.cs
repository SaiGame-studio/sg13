using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : SaiSingleton<InventoryManager>
{
    [Header("Inventory")]
    [SerializeField] protected ItemInventory choosedItem;
    public ItemInventory ChoosedItem => choosedItem;
    [SerializeField] protected List<InventoryCtrl> inventories;
    [SerializeField] protected List<ItemProfileSO> itemProfiles;

    [SerializeField] protected ItemInventory fate;
    public ItemInventory Fate => fate;

    [SerializeField] protected float foodReserve = 0;
    [SerializeField] protected float waterReserve = 0;
    [SerializeField] protected float fiberReserve = 0;

    protected virtual void FixedUpdate()
    {
        this.CheckReserve();
    }

    protected override void Start()
    {
        base.Start();
        this.AddDefaultItems();
    }

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

    public virtual ItemProfileSO GetProfileByCode(ItemCode itemCodeName)
    {
        foreach (ItemProfileSO itemProfile in this.itemProfiles)
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

    public virtual ItemInventory AddItem(ItemInventory itemInventory)
    {
        InvCodeName invCodeName = itemInventory.ItemProfile.invCodeName;
        InventoryCtrl inventoryCtrl = InventoryManager.Instance.GetByCodeName(invCodeName);
        return inventoryCtrl.AddItem(itemInventory);
    }

    public virtual ItemInventory AddItem(ItemCode itemCode, int itemCount)
    {
        ItemProfileSO itemProfile = InventoryManager.Instance.GetProfileByCode(itemCode);
        ItemInventory item = new(itemProfile, itemCount);
        this.AddItem(item);
        return item;
    }

    public virtual void DeductItem(ItemCode itemCode, int itemCount)
    {
        this.RemoveItem(itemCode, itemCount);
    }

    public virtual void RemoveItem(ItemCode itemCode, int itemCount)
    {
        ItemProfileSO itemProfile = InventoryManager.Instance.GetProfileByCode(itemCode);
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
        ItemProfileSO[] itemProfileSOs = Resources.LoadAll<ItemProfileSO>("/");
        this.itemProfiles = new List<ItemProfileSO>(itemProfileSOs);
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
        int useCount = 1;
        if (this.choosedItem.ItemID == 0) return;
        if (!this.choosedItem.CanDeduct(useCount)) return;
        if (!this.CheckPlayerNeed()) return;

        int fate = this.choosedItem.ItemProfile.fate;
        if (this.choosedItem.ItemProfile.isKarma) this.ResetFate();
        else this.AddFate(fate);

        if (this.choosedItem.ItemProfile.isFood) PlayerNeeds.Instance.SetEating(true);

        this.DeductChoosedItem(useCount);
    }

    protected virtual void DeductChoosedItem(int count)
    {
        this.choosedItem.Deduct(count);
        if(this.choosedItem.itemCount == 0) this.choosedItem = null;
    }

    public virtual void DiscardChoosedItem()
    {
        int count = 1;
        if (this.choosedItem.ItemID == 0) return;
        if (!this.choosedItem.CanDeduct(count)) return;
        this.DeductChoosedItem(count);
    }

    protected virtual bool CheckPlayerNeed()
    {
        float itemHunger = this.choosedItem.ItemProfile.hunger;
        float itemThirst = this.choosedItem.ItemProfile.thirst;
        float itemfiber = this.choosedItem.ItemProfile.fiber;
        if (!PlayerNeeds.Instance.CanEat(itemHunger)) return false;
        if (!PlayerNeeds.Instance.CanDrink(itemThirst)) return false;
        if (!PlayerNeeds.Instance.CanSew(itemfiber)) return false;

        if (itemHunger > 0) PlayerNeeds.Instance.Eat(itemHunger);
        if (itemThirst > 0) PlayerNeeds.Instance.Drink(itemThirst);
        if (itemfiber > 0) PlayerNeeds.Instance.Sew(itemfiber);
        return true;
    }

    public virtual void DeductFate(int deductNumber)
    {
        this.DeductItem(ItemCode.Fate, deductNumber);
        PlayerCtrl.Instance.Level.SetLevel(0);
    }

    public virtual void ResetFate()
    {
        int deductNumber = this.fate.itemCount;
        if (deductNumber < 0) deductNumber = 77;
        this.DeductItem(ItemCode.Fate, deductNumber + 1);
        PlayerCtrl.Instance.Level.SetLevel(0);
    }

    public virtual ItemInventory AddFate(int deductNumber)
    {
        return this.AddItem(ItemCode.Fate, deductNumber);
    }

    protected virtual void AddDefaultItems()
    {
        this.fate = this.AddFate(1);
    }

    public virtual float FoodReserve()
    {
        return this.foodReserve;
    }

    public virtual float WaterReserve()
    {
        return this.waterReserve;
    }

    public virtual float FiberReserve()
    {
        return this.fiberReserve;
    }

    protected virtual void CheckReserve()
    {
        this.foodReserve = 0;
        this.waterReserve = 0;
        this.fiberReserve = 0;
        foreach (ItemInventory itemInventory in this.Items().Items)
        {
            if (itemInventory.ItemProfile.isKarma) continue;
            this.foodReserve += itemInventory.ItemProfile.hunger * itemInventory.itemCount;
            this.waterReserve += itemInventory.ItemProfile.thirst * itemInventory.itemCount;
            this.fiberReserve += itemInventory.ItemProfile.fiber * itemInventory.itemCount;
        }
    }

    public virtual void LoadSaveData(List<ItemInventory> savedItems)
    {
        foreach (ItemInventory itemInventory in savedItems)
        {
            this.AddItem(itemInventory.ItemCode, itemInventory.itemCount);
        }
    }
}
