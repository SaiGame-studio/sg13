using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class OnRoadDespawn : Despawn<OnRoadCtrl>
{
    [SerializeField] protected bool isSeePlayer = false;
    [SerializeField] protected float seeDistance = 5f;
    [SerializeField] protected float minDistance = Mathf.Infinity;
    [SerializeField] protected float maxDistance = 52f;
    [SerializeField] protected float playerDistance = Mathf.Infinity;
    protected virtual void OnEnable()
    {
        this.Reborn();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.CheckPlayerDistance();
    }

    protected virtual void OnTriggerExit(Collider collider)
    {
        if (!collider.gameObject.CompareTag(TagCode.Player.ToString())) return;
        this.TriggerDespawn(collider);
    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.isDespawnByTime = false;   
    }

    protected virtual void TriggerDespawn(Collider collider)
    {
        if (this.isSeePlayer) this.DoDespawn();
    }

    protected virtual void CheckPlayerDistance()
    {
        if(this.isSeePlayer) return; 
        this.playerDistance = Vector3.Distance(transform.position, PlayerCtrl.Instance.transform.position);
        if(this.playerDistance < this.minDistance) this.minDistance = playerDistance;
        if (this.playerDistance < this.seeDistance) this.isSeePlayer = true;

        if (this.playerDistance > this.maxDistance) this.DoDespawn();
    }

    protected virtual void Reborn()
    {
        this.isSeePlayer = false;
    }
}
