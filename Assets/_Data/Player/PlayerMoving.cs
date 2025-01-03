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
    [SerializeField] protected bool isMoving = false;
    [SerializeField] protected bool isFinish = false;
    [SerializeField] protected bool isLoopPath = true;

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
        this.Moving();
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

    protected virtual void Moving()
    {

        if (!this.canMove)
        {
            this.ctrl.Agent.isStopped = true;
            return;
        }

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
    }

    protected virtual void OnReborn()
    {
        this.isFinish = false;
        this.currentPoint = null;
    }
}
