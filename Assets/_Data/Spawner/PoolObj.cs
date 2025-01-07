using UnityEngine;

public abstract class PoolObj : SaiBehaviour 
{
    [Header("Pool Obj")]
    [SerializeField] protected DespawnBase despawn;
    public DespawnBase Despawn => despawn;

    public abstract string GetName();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadDespawn();
    }

    protected virtual void LoadDespawn()
    {
        if (this.despawn != null) return;
        Transform obj = transform.Find("Despawn");
        if (obj == null)
        {
            obj = new GameObject("Despawn").transform;
            obj.parent = transform;
        }

        this.despawn = transform.GetComponentInChildren<DespawnBase>();
        Debug.Log(transform.name + ": LoadDespawn", gameObject);
    }
}
