using UnityEngine;
using UnityEngine.AI;


[RequireComponent (typeof(NavMeshAgent))]
public abstract class VillagerCtrl : PoolObj
{
    [SerializeField] protected Transform model;

    [SerializeField] protected NavMeshAgent agent;
    public NavMeshAgent Agent { get { return agent; } }

    [SerializeField] protected Animator animator;
    public Animator Animator { get { return animator; } }

    [SerializeField] protected VillagerMoving moving;
    public VillagerMoving Moving { get { return moving; } }

    public override string GetName()
    {
        return VillagerCode.Villager1.ToString();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNavMeshAgent();
        this.LoadModel();
        this.LoadAnimator();
        this.LoadMoving();
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
        this.animator.applyRootMotion = true;
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }
}
