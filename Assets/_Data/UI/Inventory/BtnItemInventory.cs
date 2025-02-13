using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnItemInventory : ButttonAbstract
{
    [SerializeField] protected bool isSelected = false;
    [SerializeField] protected Image itemBackground;
    [SerializeField] protected Image itemIcon;
    [SerializeField] protected TextMeshProUGUI txtItemCount;

    [SerializeField] protected ItemInventory itemInventory;
    public ItemInventory ItemInventory => itemInventory;

    protected virtual void FixedUpdate()
    {
        this.ItemUpdating();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadItemBackground();
        this.LoadItemIcon();
        this.LoadItemCount();
    }

    protected virtual void LoadItemBackground()
    {
        if (this.itemBackground != null) return;
        this.itemBackground = GetComponent<Image>();
        Debug.Log(transform.name + ": LoadItemBackground", gameObject);
    }

    protected virtual void LoadItemIcon()
    {
        if (this.itemIcon != null) return;
        this.itemIcon = transform.Find("ItemIcon").GetComponent<Image>();
        Debug.Log(transform.name + ": LoadItemIcon", gameObject);
    }

    protected virtual void LoadItemCount()
    {
        if (this.txtItemCount != null) return;
        this.txtItemCount = transform.Find("ItemCount").Find("TextCount").GetComponent<TextMeshProUGUI>();
        Debug.Log(transform.name + ": LoadItemCount", gameObject);
    }

    public virtual void SetItem(ItemInventory itemInventory)
    {
        this.itemInventory = itemInventory;
        this.itemIcon.sprite = this.itemInventory.ItemProfile.image;
    }

    public override void OnClick()
    {
        this.ChooseThisItem();
    }

    public override void OnDoubleClick()
    {
        //this.ChooseThisItem();
        InventoryManager.Instance.UseChoosedItem();
    }

    public virtual void ChooseThisItem()
    {
        UIInventory.Instance.CurrentBtnItem?.UnChoose();
        UIInventory.Instance.SetCurrentItem(this);
        this.isSelected = true;
    }

    public virtual void UnChoose()
    {
        this.isSelected = false;    }

    protected virtual void ItemUpdating()
    {
        this.txtItemCount.text = this.itemInventory.itemCount.ToString();
        if (this.itemInventory.itemCount == 0) Destroy(gameObject);

        this.itemBackground.enabled = this.isSelected; 
    }
}