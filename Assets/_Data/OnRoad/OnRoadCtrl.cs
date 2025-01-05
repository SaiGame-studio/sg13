using UnityEngine;
using UnityEngine.UI;

public class OnRoadCtrl : PoolObj
{
    [Header("On Road")]
    [SerializeField] protected Transform canvas;
    public Transform Canvas { get { return canvas; } }

    [SerializeField] protected Button giveButton;
    public Button GiveButton { get { return giveButton; } }

    [SerializeField] protected Image image;
    public Image Image { get { return image; } }

    [SerializeField] protected ItemCode itemGive;
    public ItemCode ItemGive { get { return itemGive; } }

    [SerializeField] protected UIImages itemProfileGive;

    protected virtual void OnEnable()
    {
        this.Reborn();
    }

    public override string GetName()
    {
        return OnRoadCode.OnRoad.ToString();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCanvas();
        this.LoadGiveButton();
        this.LoadImage();
    }
    protected virtual void LoadImage()
    {
        if (this.image != null) return;
        this.image = this.giveButton.transform.Find("Image").GetComponent<Image>();
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

    public virtual void RandomItem()
    {
        this.itemGive = InventoryManager.Instance.RandomItem();
        this.itemProfileGive = InventoryManager.Instance.GetProfileByCode(this.itemGive);
        this.image.sprite = this.itemProfileGive.image;
    }

    protected virtual void Reborn()
    {
        this.giveButton.gameObject.SetActive(false);
    }

    public virtual void GiveItem()
    {
        InventoryManager.Instance.AddItem(this.itemGive, 1);
        this.giveButton.gameObject.SetActive(false);
        this.giveButton.enabled = false;
    }
}
