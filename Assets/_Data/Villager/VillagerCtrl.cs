using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


[RequireComponent (typeof(NavMeshAgent))]
public class VillagerCtrl : PoolObj
{
    [SerializeField] protected Transform model;

    [SerializeField] protected NavMeshAgent agent;
    public NavMeshAgent Agent { get { return agent; } }

    [SerializeField] protected Animator animator;
    public Animator Animator { get { return animator; } }

    [SerializeField] protected VillagerMoving moving;
    public VillagerMoving Moving { get { return moving; } }

    [SerializeField] protected Transform canvas;
    public Transform Canvas { get { return canvas; } }

    [SerializeField] protected Button giveButton;
    public Button GiveButton { get { return giveButton; } }

    [SerializeField] protected Image image;
    public Image Image { get { return image; } }

    [SerializeField] protected ItemCode itemGive;
    [SerializeField] protected ItemProfileSO itemProfileGive;

    protected virtual void OnEnable()
    {
        this.Reborn();
    }

    public override string GetName()
    {
        return VillagerCode.Villager.ToString();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNavMeshAgent();
        this.LoadModel();
        this.LoadAnimator();
        this.LoadMoving();
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
        this.canvas = transform.Find("VillagerCanvas");
        Debug.Log(transform.name + ": LoadRectTransform", gameObject);
    }

    protected virtual void LoadGiveButton()
    {
        if (this.giveButton != null) return;
        this.giveButton = this.canvas.Find("BtnGiveItem").GetComponent<Button>();
        Debug.Log(transform.name + ": LoadGiveButton", gameObject);
    }

    protected virtual void LoadMoving()
    {
        if (this.moving != null) return;
        this.moving = GetComponentInChildren<VillagerMoving>();
        Debug.Log(transform.name + ": LoadMoving", gameObject);
    }

    protected virtual void LoadNavMeshAgent()
    {
        if (this.agent != null) return;
        this.agent = GetComponent<NavMeshAgent>();
        this.agent.speed = 4f;
        this.agent.angularSpeed = 200f;
        this.agent.acceleration = 150f;
        this.agent.stoppingDistance = 1f;
        Debug.Log(transform.name + ": LoadNavMeshAgent", gameObject);
    }

    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model");
        this.model.localPosition = new Vector3(0f, 0f, 0f);
        Debug.Log(transform.name + ": LoadModel", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = this.model.GetComponent<Animator>();
        this.animator.applyRootMotion = false;
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }

    public virtual void RandomItem()
    {
        this.itemGive = InventoryManager.Instance.RandomItem();
        this.itemProfileGive = InventoryManager.Instance.GetProfileByCode(this.itemGive);
        this.image.sprite = this.itemProfileGive.image;
    }

    protected virtual void Reborn()
    {
        Vector3 position = this.model.localPosition;
        position.y = 0f;
        this.model.localPosition = position;
        this.giveButton.gameObject.SetActive(false);
    }

    public virtual void GiveItem()
    {
        InventoryManager.Instance.AddItem(this.itemGive, 1);
        this.giveButton.gameObject.SetActive(false);
        this.giveButton.enabled = false;
    }
}
