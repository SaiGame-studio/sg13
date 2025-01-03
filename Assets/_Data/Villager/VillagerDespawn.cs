using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class VillagerDespawn : Despawn<VillagerCtrl>
{
    protected virtual void OnTriggerExit(Collider collider)
    {
        this.TriggerDespawn(collider);

    }

    protected override void ResetValue()
    {
        base.ResetValue();
        this.isDespawnByTime = false;   
    }

    protected virtual void TriggerDespawn(Collider collider)
    {
        if (!collider.gameObject.CompareTag(TagCode.Player.ToString())) return;
        this.DoDespawn();
    }
}
