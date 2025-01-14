using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMoving : SaiBehaviour
{
    [SerializeField] protected PlayerCtrl ctrl;
    [SerializeField] protected string pathName = "path_0";
    [SerializeField] protected PathCtrl path;
    [SerializeField] protected Point currentPoint;
    [SerializeField] protected float pointDistance = Mathf.Infinity;
    [SerializeField] protected float stopDistance = 1f;
    [SerializeField] protected bool canMove = true;
    [SerializeField] protected bool isFinish = false;
    [SerializeField] protected bool isLoopPath = true;

    [SerializeField] protected bool isMoving = false;
    public bool IsMoving { get { return isMoving; } }

    [SerializeField] protected bool isSitting = false;
    public bool IsSitting { get { return isSitting; } }


    protected virtual void OnEnable()
    {
        this.OnReborn();
    }

    protected override void Start()
    {
        this.LoadPath();
    }

    void FixedUpdate()
    {
        this.UpdateMoving();
        this.CheckMoving();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCtrl();
    }

    protected virtual void LoadCtrl()
    {
        if (this.ctrl != null) return;
        this.ctrl = transform.parent.GetComponent<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadCtrl", gameObject);
    }

    protected virtual void UpdateMoving()
    {
        bool isAlive = this.ctrl.Needs.IsAlive;
        if (!this.canMove || this.isSitting || !isAlive)
        {
            this.ctrl.Agent.isStopped = true;
            return;
        }

        this.FixModel();
        this.FindNextPoint();

        if (this.currentPoint == null || this.isFinish == true)
        {
            this.ctrl.Agent.isStopped = true;
            return;
        }

        this.ctrl.Agent.isStopped = false;
        this.ctrl.Agent.SetDestination(this.currentPoint.transform.position);
    }

    protected virtual void FindNextPoint()
    {
        if (this.currentPoint == null) this.currentPoint = this.path.GetPoint(0);

        this.pointDistance = Vector3.Distance(transform.position, this.currentPoint.transform.position);
        if (this.pointDistance < this.stopDistance)
        {
            this.currentPoint = this.currentPoint.NextPoint;
            if (this.currentPoint == null)
            {
                if (this.isLoopPath) this.currentPoint = this.path.GetPoint(0);
                else this.isFinish = true;
            }
        }
    }

    protected virtual void LoadPath()
    {
        if (this.path != null) return;
        this.path = PathsManager.Instance.GetPath(this.pathName);
        //Debug.LogWarning(transform.name + ": LoadPath", gameObject);
    }

    protected virtual void CheckMoving()
    {
        if (this.ctrl.Agent.velocity.magnitude > 0.1f) this.isMoving = true;
        else this.isMoving = false;

        this.ctrl.Animator.SetBool("isMoving", this.isMoving);
        this.ctrl.Animator.SetBool("isSitting", this.isSitting);
    }

    protected virtual void OnReborn()
    {
        this.isFinish = false;
        this.currentPoint = null;
    }

    protected virtual void DelayCanMove()
    {
        this.canMove = true;
    }

    public virtual void StandUp()
    {
        this.isSitting = false;
        Invoke(nameof(this.DelayCanMove), 1.2f);
        PlayerNeeds.Instance.CheckEating();
    }

    public virtual void SitDown()
    {
        this.isSitting = true;
        this.canMove = false;
    }

    public virtual void ToggleWalking()
    {
        this.canMove = !this.canMove;
    }

    protected virtual void FixModel()
    {
        this.ctrl.Model.localPosition = Vector3.zero;
        this.ctrl.Model.localRotation = Quaternion.identity;
    }
}
