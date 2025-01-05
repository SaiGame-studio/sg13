using System.Collections.Generic;
using UnityEngine;

public class VillagerPoint : OnRoadPoint
{
    [SerializeField] protected Transform standPoint;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadStandPoint();
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

    protected override void Spawn()
    {
        OnRoadCtrl onRoadPrefab = this.GetRandom();
        if (onRoadPrefab == null)
        {
            Invoke(nameof(this.Spawn), 0.5f);
            return;
        }

        OnRoadCtrl onRoadObj = OnRoadsCtrl.Instance.Spawner.Spawn(onRoadPrefab, transform.position);
        VillagerCtrl villagerObj = (VillagerCtrl)onRoadObj;
        villagerObj.Moving.SetTarget(this.standPoint);
        villagerObj.RandomItem();
        villagerObj.SetActive(true);
    }


    protected virtual OnRoadCtrl GetRandom()
    {
        List<string> onRoads = new()
        {
            OnRoadCode.Villager.ToString(),
            OnRoadCode.Villager1.ToString(),
            OnRoadCode.Villager2.ToString(),
        };

        OnRoadCtrl onRoadPrefab = OnRoadsCtrl.Instance.Spawner.PoolPrefabs.GetRandom();
        if (onRoadPrefab == null) return null;
        if (onRoads.Contains(onRoadPrefab.GetName())) return onRoadPrefab;
        return null;
    }
}
