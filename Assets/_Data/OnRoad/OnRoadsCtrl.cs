using UnityEngine;

public class OnRoadsCtrl : SaiSingleton<OnRoadsCtrl>
{
    [SerializeField] protected OnRoadSpawner spawner;
    public OnRoadSpawner Spawner { get { return spawner; } }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawner();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GetComponent<OnRoadSpawner>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }
}
