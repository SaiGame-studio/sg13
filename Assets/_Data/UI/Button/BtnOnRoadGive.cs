using UnityEngine;

public class BtnOnRoadGive : ButttonAbstract
{
    [SerializeField] protected OnRoadCtrl onRoadCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadOnRoadCtrl();
    }

    protected virtual void LoadOnRoadCtrl()
    {
        if(this.onRoadCtrl != null) return;
        this.onRoadCtrl = transform.parent.parent.GetComponent<OnRoadCtrl>();
        Debug.LogWarning(transform.name + ": LoadOnRoadCtrl", gameObject);
    }

    public override void OnClick()
    {
        this.onRoadCtrl.GiveItem();
    }
}
