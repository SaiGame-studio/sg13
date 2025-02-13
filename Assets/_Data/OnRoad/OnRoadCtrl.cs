using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class OnRoadCtrl : PoolObj
{
    [Header("On Road")]
    [SerializeField] protected Transform canvas;
    public Transform Canvas { get { return canvas; } }

    [SerializeField] protected Button giveButton;
    public Button GiveButton { get { return giveButton; } }

    [SerializeField] protected Image giveImage;
    public Image GiveImage { get { return giveImage; } }

    [SerializeField] protected ItemCode itemGive;
    public ItemCode ItemGive { get { return itemGive; } }

    [SerializeField] protected ItemProfileSO itemProfileGive;
    [SerializeField] protected InventoryManager inventoryManager;

    [SerializeField]
    protected List<ItemCode> itemsGive = new();

    protected virtual void OnEnable()
    {
        this.Reborn();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCanvas();
        this.LoadGiveButton();
        this.LoadImage();
        this.LoadInventoryManager();
    }

    protected virtual void LoadInventoryManager()
    {
        if (this.inventoryManager != null) return;
        this.inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        Debug.LogWarning(transform.name + ": LoadInventoryManager", gameObject);
    }

    protected virtual void LoadImage()
    {
        if (this.giveImage != null) return;
        this.giveImage = this.giveButton.transform.Find("Image").GetComponent<Image>();
        Debug.LogWarning(transform.name + ": LoadImage", gameObject);
    }

    protected virtual void LoadCanvas()
    {
        if (this.canvas != null) return;
        this.canvas = transform.Find("ItemCanvas");
        Debug.Log(transform.name + ": LoadRectTransform", gameObject);
    }

    protected virtual void LoadGiveButton()
    {
        if (this.giveButton != null) return;
        this.giveButton = this.canvas.Find("BtnGiveItem").GetComponent<Button>();
        Debug.Log(transform.name + ": LoadGiveButton", gameObject);
    }

    public virtual void RandomAndShowItem()
    {
        this.itemGive = this.RandomItem();
        this.itemProfileGive = this.inventoryManager.GetProfileByCode(this.itemGive);
        this.giveImage.sprite = this.itemProfileGive.image;
        this.ShowButtonToLoot();
    }

    protected virtual void Reborn()
    {
        this.RandomAndShowItem();
    }

    public virtual void GiveItem()
    {
        InventoryManager.Instance.AddItem(this.itemGive, 1);
        if (this.itemProfileGive.isInstanceKarma)
        {
            int deductNumber = this.itemProfileGive.fate;
            InventoryManager.Instance.DeductFate(deductNumber);
        }
        this.GiveButton.gameObject.SetActive(false);
    }

    protected virtual ItemCode RandomItem()
    {
        return this.inventoryManager.RandomItem(this.GetGiveItems());
    }

    protected virtual List<ItemCode> GetGiveItems()
    {
        return this.itemsGive;
    }

    protected virtual void ShowButtonToLoot()
    {
        int rand = Random.Range(0, 100);
        if (rand > this.GetLuckRate()) return;
        this.GiveButton.gameObject.SetActive(true);
    }

    protected virtual int GetLuckRate()
    {
        return 80;
    }
}
