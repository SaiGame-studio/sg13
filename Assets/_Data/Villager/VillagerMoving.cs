using UnityEngine;

public class VillagerMoving : SaiBehaviour
{
    [SerializeField] protected VillagerCtrl ctrl;
    [SerializeField] protected Transform target;
    [SerializeField] protected bool canMove = true;
    [SerializeField] protected bool isMoving = false;
    [SerializeField] protected bool isFinish = false;
    [SerializeField] protected float distanceLimit = 1.6f;
    [SerializeField] protected int maxIdleState = 4;
    [SerializeField] protected int maxRunState = 3;

    void FixedUpdate()
    {
        this.Moving();
        this.CheckMoving();
    }

    protected virtual void OnEnable()
    {
        this.Reborn();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = transform.parent.GetComponent<VillagerCtrl>();
        Debug.LogWarning(transform.name + ": LoadCtrl", gameObject);
    }


    public virtual void SetTarget(Transform target)
    {
        this.target = target;
    }

    protected virtual void Moving()
    {

        if (!this.canMove || this.target == null || this.isFinish)
        {
            this.ctrl.transform.rotation = this.target.rotation;
            this.ctrl.Agent.isStopped = true;
            return;
        }

        this.ctrl.Agent.isStopped = false;
        this.ctrl.Agent.SetDestination(this.target.transform.position);
    }

    protected virtual void CheckMoving()
    {
        if (this.ctrl.Agent.velocity.magnitude > 0.1f) this.isMoving = true;
        else this.isMoving = false;

        float distance = Vector3.Distance(transform.position, this.target.transform.position);
        if (distance <= this.distanceLimit) this.isFinish = true;

        this.ctrl.Animator.SetBool("isMoving", this.isMoving);
    }

    protected virtual void Reborn()
    {
        this.isFinish = false;
        this.ctrl.Animator.SetFloat("idleState", Random.Range(0, this.maxIdleState));
        this.ctrl.Animator.SetFloat("runState", Random.Range(0, this.maxRunState));
    }
}
