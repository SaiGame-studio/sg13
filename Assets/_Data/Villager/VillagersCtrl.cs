using UnityEngine;

public class VillagersCtrl : SaiSingleton<VillagersCtrl>
{
    [SerializeField] protected VillagersSpawner spawner;
    public VillagersSpawner Spawner { get { return spawner; } }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawner();
    }

    protected virtual void LoadSpawner()
    {
        if (this.spawner != null) return;
        this.spawner = GetComponent<VillagersSpawner>();
        Debug.LogWarning(transform.name + ": LoadSpawner", gameObject);
    }
}
