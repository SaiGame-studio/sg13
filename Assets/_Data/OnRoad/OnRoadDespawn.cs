using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class OnRoadDespawn : Despawn<OnRoadCtrl>
{
    [SerializeField] protected bool isSeePlayer = false;
    [SerializeField] protected float seeDistance = 5f;
    [SerializeField] protected float playerDistance = Mathf.Infinity;
    protected virtual void OnEnable()
    {
        this.Reborn();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        this.CheckIsSeePlayer();
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

    protected virtual void CheckIsSeePlayer()
    {
        if(this.isSeePlayer) return; 
        this.playerDistance = Vector3.Distance(transform.position, PlayerCtrl.Instance.transform.position);
        if (this.playerDistance < this.seeDistance) this.isSeePlayer = true;
    }

    protected virtual void Reborn()
    {
        this.isSeePlayer = false;
    }
}
