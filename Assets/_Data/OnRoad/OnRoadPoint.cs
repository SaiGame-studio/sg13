using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class OnRoadPoint : SaiBehaviour
{
    [Header("OnRoad")]
    [SerializeField] protected int spawnCount = 1;
    [SerializeField] protected SphereCollider _collider;

    protected abstract void Spawn();

    protected virtual void OnTriggerEnter(Collider collider)
    {
        this.OnRoadSpawn(collider);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
    }

    protected virtual void LoadCollider()
    {
        if (this._collider != null) return;
        this._collider = GetComponent<SphereCollider>();
        this._collider.isTrigger = true;
        Debug.LogWarning(transform.name + ": LoadCollider", gameObject);
    }

    protected virtual void OnRoadSpawn(Collider collider)
    {
        if (!collider.gameObject.CompareTag(TagCode.Player.ToString())) return;
        for (int i = 0; i < this.spawnCount; i++)
        {
            Invoke(nameof(this.Spawn), i * (1 + i));
        }
    }
}
