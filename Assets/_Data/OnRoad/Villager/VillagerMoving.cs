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
    [SerializeField] protected int runState = 0;
    [SerializeField] protected int idleState = 0;

    private void LateUpdate()
    {
        this.UpdateAnimation();
    }

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

    protected virtual void UpdateAnimation()
    {
        this.ctrl.Animator.SetBool("isMoving", this.isMoving);
    }

    protected virtual void CheckMoving()
    {
        if (this.isFinish) return;

        if (this.ctrl.Agent.velocity.magnitude > 0.1f) this.isMoving = true;

        float distance = Vector3.Distance(transform.position, this.target.transform.position);
        if (distance <= this.distanceLimit)
        {
            this.isFinish = true;
            this.isMoving = false;
            Invoke(nameof(this.ShowGiveButton), 1f);
        }
    }

    protected virtual void ShowGiveButton()
    {
        if (this.idleState == (int)VillagerIdleCode.Standing) return;
        this.ctrl.GiveButton.gameObject.SetActive(true);
    }

    protected virtual void Reborn()
    {
        this.isFinish = false;
        this.idleState = Random.Range(0, this.maxIdleState);
        this.runState = Random.Range(0, this.maxRunState);
        this.ctrl.Animator.SetFloat("idleState", this.idleState);
        this.ctrl.Animator.SetFloat("runState", this.runState);
    }
}
