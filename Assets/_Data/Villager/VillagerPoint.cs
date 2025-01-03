using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class VillagerPoint : SaiBehaviour
{
    [SerializeField] protected int spawnCount = 1;
    [SerializeField] protected SphereCollider _collider;
    [SerializeField] protected Transform standPoint;

    protected virtual void OnTriggerEnter(Collider collider)
    {
        this.SpawnVillager(collider);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCollider();
        this.LoadStandPoint();
    }

    protected virtual void LoadCollider()
    {
        if (this._collider != null) return;
        this._collider = GetComponent<SphereCollider>();
        this._collider.isTrigger = true;
        Debug.LogWarning(transform.name + ": LoadCollider", gameObject);
    }

    protected virtual void LoadStandPoint()
    {
        if (this.standPoint != null) return;
        this.standPoint = transform.Find("StandPoint");
        if (this.standPoint == null)
        {
            this.standPoint = new GameObject("StandPoint").transform;
            this.standPoint.parent = transform;
        }
        Debug.LogWarning(transform.name + ": LoadStandPoint", gameObject);
    }

    protected virtual void SpawnVillager(Collider collider)
    {
        if (!collider.gameObject.CompareTag(TagCode.Player.ToString())) return;
        for (int i = 0; i < this.spawnCount; i++)
        {
            Invoke(nameof(this.Spawn), i * (1 + i));
        }
    }

    protected virtual void Spawn()
    {
        VillagerCtrl villager = VillagersCtrl.Instance.Spawner.PoolPrefabs.GetRandom();
        VillagerCtrl villagerObj = VillagersCtrl.Instance.Spawner.Spawn(villager, transform.position);
        villagerObj.Moving.SetTarget(this.standPoint);
        villagerObj.SetActive(true);

    }
}
