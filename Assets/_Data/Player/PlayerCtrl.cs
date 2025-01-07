using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerCtrl : SaiSingleton<PlayerCtrl>
{
    [SerializeField] protected Transform model;
    public Transform Model => model;

    [SerializeField] protected NavMeshAgent agent;
    public NavMeshAgent Agent { get { return agent; } }

    [SerializeField] protected Animator animator;
    public Animator Animator { get { return animator; } }

    [SerializeField] protected PlayerMoving moving;
    public PlayerMoving Moving { get { return moving; } }

    [SerializeField] protected PlayerLevel level;
    public PlayerLevel Level { get { return level; } }

    [SerializeField] protected PlayerNeeds needs;
    public PlayerNeeds Needs { get { return needs; } }

    [SerializeField] protected bool isHasKarma = false;
    public bool IsHasKarma => isHasKarma;

    [SerializeField] protected ItemInventory fate;

    protected virtual void FixedUpdate()
    {
        this.StatusUpdating();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNavMeshAgent();
        this.LoadModel();
        this.LoadAnimator();
        this.LoadPlayerMoving();
        this.LoadPlayerLevel();
        this.LoadPlayerNeeds();
    }

    protected virtual void LoadPlayerNeeds()
    {
        if (this.needs != null) return;
        this.needs = GetComponentInChildren<PlayerNeeds>();
        Debug.Log(transform.name + ": LoadPlayerNeeds", gameObject);
    }

    protected virtual void LoadPlayerLevel()
    {
        if (this.level != null) return;
        this.level = GetComponentInChildren<PlayerLevel>();
        Debug.Log(transform.name + ": LoadPlayerLevel", gameObject);
    }

    protected virtual void LoadPlayerMoving()
    {
        if (this.moving != null) return;
        this.moving = GetComponentInChildren<PlayerMoving>();
        Debug.Log(transform.name + ": LoadPlayerMoving", gameObject);
    }

    protected virtual void LoadNavMeshAgent()
    {
        if (this.agent != null) return;
        this.agent = GetComponent<NavMeshAgent>();
        this.agent.speed = 1.6f;
        this.agent.angularSpeed = 200f;
        this.agent.acceleration = 150f;
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
        this.animator.applyRootMotion = true;
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void StatusUpdating()
    {
        if(this.GetFate() == null) this.isHasKarma = false;
        else this.isHasKarma = this.GetFate().itemCount < 0;
    }

    public virtual ItemInventory GetFate()
    {
        if(this.fate == null || this.fate.ItemID == 0 || this.fate.itemCount == 0) this.fate = InventoryManager.Instance.Monies().GetItem(ItemCode.Fate);
        return this.fate;
    }
}
