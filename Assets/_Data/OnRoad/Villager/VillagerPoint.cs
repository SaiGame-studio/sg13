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

    protected override OnRoadCtrl Spawn()
    {
        OnRoadCtrl onRoadObj = base.Spawn();
        if (onRoadObj == null) return null;
        VillagerCtrl villagerObj = (VillagerCtrl)onRoadObj;
        villagerObj.Moving.SetTarget(this.standPoint);
        return onRoadObj;
    }

    protected override List<string> GetRandomList()
    {
        return new()
        {
            OnRoadCode.Villager.ToString(),
            OnRoadCode.Villager1.ToString(),
            OnRoadCode.Villager2.ToString(),
        };
    }
}
