using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : SaiSingleton<UIInventory>
{
    protected bool isShow = true;
    protected bool IsShow => isShow;
    [SerializeField] protected Transform showHide;
    [SerializeField] protected BtnItemInventory defaultItemInventoryUI;
    [SerializeField] protected Transform btnUse;
    [SerializeField] protected Image btnUseImage;
    [SerializeField] protected TextMeshProUGUI txtItemName;
    [SerializeField] protected ImagesSO btnImages;

    [SerializeField] protected BtnItemInventory currentBtnItem;
    public BtnItemInventory CurrentBtnItem => currentBtnItem;

    [SerializeField] protected List<BtnItemInventory> btnItems = new();

    protected override void Start()
    {
        base.Start();
        this.HideDefaultItemInventory();
        this.Hide();
    }

    protected virtual void FixedUpdate()
    {
        this.ItemsUpdating();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBtnItemInventory();
        this.LoadShowHide();
        this.LoadBtnEat();
        this.LoadBtnImages();
        this.LoadTxtItemName();
    }

    protected virtual void LoadTxtItemName()
    {
        if (this.txtItemName != null) return;
        this.txtItemName = this.showHide.Find("TxtItemName").GetComponent<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadBtnImages", gameObject);
    }

    protected virtual void LoadBtnImages()
    {
        if (this.btnImages != null) return;
        this.btnImages = Resources.Load<ImagesSO>("BtnUseImages");
        Debug.LogWarning(transform.name + ": LoadBtnImages", gameObject);
    }

    protected virtual void LoadBtnEat()
    {
        if (this.btnUse != null) return;
        this.btnUse = this.showHide.Find("BtnUseItem");
        this.btnUseImage = this.btnUse.GetComponent<Image>();
        this.btnUse.gameObject.SetActive(false);
        Debug.Log(transform.name + ": LoadBtnEat", gameObject);
    }

    protected virtual void LoadShowHide()
    {
        if (this.showHide != null) return;
        this.showHide = transform.Find("ShowHide");
        Debug.Log(transform.name + ": LoadShowHide", gameObject);
    }

    protected virtual void LoadBtnItemInventory()
    {
        if (this.defaultItemInventoryUI != null) return;
        this.defaultItemInventoryUI = GetComponentInChildren<BtnItemInventory>();
        Debug.Log(transform.name + ": LoadBtnItemInventory", gameObject);
    }

    public virtual void Show()
    {
        this.isShow = true;
        this.showHide.gameObject.SetActive(this.isShow);
        PlayerCtrl.Instance.Moving.SitDown();
    }

    public virtual void Hide()
    {
        this.showHide.gameObject.SetActive(false);
        this.isShow = false;
        PlayerCtrl.Instance.Moving.StandUp();
    }

    public virtual void Toggle()
    {
        if (this.isShow) this.Hide();
        else this.Show();
    }

    protected virtual void HideDefaultItemInventory()
    {
        this.defaultItemInventoryUI.gameObject.SetActive(false);
    }

    protected virtual void ItemsUpdating()
    {
        if (!this.isShow) return;

        this.ClearEmptyItems();

        //this.UpdateFromInventory(InventoryManager.Instance.Monies());
        this.UpdateFromInventory(InventoryManager.Instance.Items());

        this.ShowUseButton();
    }

    protected virtual void ClearEmptyItems()
    {
        BtnItemInventory item;
        for (int i = 0; i < this.btnItems.Count; i++)
        {
            item = this.btnItems[i];
            if (item != null) continue;
            this.btnItems.RemoveAt(i);
            break;
        }
    }

    protected virtual void ShowUseButton()
    {
        bool eatable = true;
        ItemInventory choosedItem = InventoryManager.Instance.ChoosedItem;
        if (choosedItem.ItemID == 0) return;

        if (!choosedItem.ItemProfile.useable) eatable = false;
        if (choosedItem.ItemProfile.isKarma) this.btnUseImage.sprite = this.btnImages.images[1];
        else this.btnUseImage.sprite = this.btnImages.images[0];

        if (choosedItem.ItemProfile.isFood)
        {
            if (!DayNightCycle.Instance.IsEatTime) eatable = false;
            if (PlayerNeeds.Instance.IsFinishEat()) eatable = false;
        }

        if (PlayerNeeds.Instance.IsSleeping()) eatable = false;

        this.txtItemName.text = this.GetItemInfo(choosedItem);

        if (choosedItem.ItemProfile.isKarma) this.txtItemName.color = Color.red;
        else this.txtItemName.color = Color.white;

        this.btnUse.gameObject.SetActive(eatable);
    }

    protected virtual string GetItemInfo(ItemInventory choosedItem)
    {
        string itemName = Language.T(choosedItem.GetItemName());
        string karmaString = Language.T("violation");
        string meritString = Language.T("merit");

        if (choosedItem.ItemProfile.isKarma) return $"{itemName}: {karmaString}";

        string fateSign = "+";
        string fateCount = fateSign + choosedItem.ItemProfile.fate.ToString();
        return $"{itemName}: {fateCount} {meritString}";
    }

    protected virtual void UpdateFromInventory(InventoryCtrl itemInvCtrl)
    {
        foreach (ItemInventory itemInventory in itemInvCtrl.Items)
        {
            if (itemInventory.itemCount > 0) this.AddItemToUI(itemInventory);
        }
    }

    protected virtual void AddItemToUI(ItemInventory itemInventory)
    {
        BtnItemInventory newBtnItem = this.GetExistItem(itemInventory);
        if (newBtnItem == null)
        {
            newBtnItem = Instantiate(this.defaultItemInventoryUI);
            newBtnItem.transform.SetParent(this.defaultItemInventoryUI.transform.parent);
            newBtnItem.SetItem(itemInventory);
            newBtnItem.transform.localScale = new Vector3(1, 1, 1);
            newBtnItem.gameObject.SetActive(true);
            newBtnItem.name = itemInventory.GetItemName() + "-" + itemInventory.ItemID;
            this.btnItems.Add(newBtnItem);
        }
    }

    protected virtual BtnItemInventory GetExistItem(ItemInventory itemInventory)
    {
        foreach (BtnItemInventory itemInvUI in this.btnItems)
        {
            if (itemInvUI.ItemInventory.ItemID == itemInventory.ItemID) return itemInvUI;
        }
        return null;
    }

    public virtual void SetCurrentItem(BtnItemInventory currentBtnItem)
    {
        this.currentBtnItem = currentBtnItem;
        InventoryManager.Instance.SetChoosedItem(currentBtnItem.ItemInventory);
    }
}