using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public abstract class OnRoadPoint : SaiBehaviour
{
    [Header("OnRoad")]
    [SerializeField] protected SphereCollider _collider;

    protected abstract List<string> GetRandomList();

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (!collider.gameObject.CompareTag(TagCode.Player.ToString())) return;
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

    protected virtual OnRoadCtrl GetRandom()
    {
        List<string> onRoads = this.GetRandomList();
        OnRoadCtrl onRoadPrefab = OnRoadsCtrl.Instance.Spawner.PoolPrefabs.GetRandom();
        if (onRoadPrefab == null) return null;
        if (onRoads.Contains(onRoadPrefab.GetName())) return onRoadPrefab;
        return null;
    }

    protected virtual void OnRoadSpawn(Collider collider)
    {
        this.Spawn();
    }

    protected virtual OnRoadCtrl Spawn()
    {
        OnRoadCtrl onRoadPrefab = this.GetRandom();
        if (onRoadPrefab == null)
        {
            Invoke(nameof(this.Spawn), 0.5f);
            return null;
        }

        OnRoadCtrl onRoadObj = OnRoadsCtrl.Instance.Spawner.Spawn(onRoadPrefab, transform.position);
        onRoadObj.SetActive(true);

        return onRoadObj;
    }
}
