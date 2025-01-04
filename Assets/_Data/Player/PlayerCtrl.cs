using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerCtrl : SaiSingleton<PlayerCtrl>
{
    [SerializeField] protected Transform model;

    [SerializeField] protected NavMeshAgent agent;
    public NavMeshAgent Agent { get { return agent; } }

    [SerializeField] protected Animator animator;
    public Animator Animator { get { return animator; } }

    [SerializeField] protected PlayerMoving moving;
    public PlayerMoving Moving { get { return moving; } }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadNavMeshAgent();
        this.LoadModel();
        this.LoadAnimator();
        this.LoadPlayerMoving();
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
        this.animator.applyRootMotion = false;
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }
}
