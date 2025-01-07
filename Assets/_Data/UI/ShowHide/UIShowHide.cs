using UnityEngine;

public abstract class UIShowHide : SaiBehaviour
{
    [SerializeField] protected Transform showHide;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShowHide();
    }

    protected virtual void LoadShowHide()
    {
        if (this.showHide != null) return;
        this.showHide = transform.Find("ShowHide");
        Debug.LogWarning(transform.name + ": LoadShowHide", gameObject);
    }
}
