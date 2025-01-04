using UnityEngine;
using UnityEngine.UI;

public class BtnVillagerGive : ButttonAbstract
{
    [SerializeField] protected VillagerCtrl villagerCtrl;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadVillagerCtrl();
    }

    protected virtual void LoadVillagerCtrl()
    {
        if(this.villagerCtrl != null) return;
        this.villagerCtrl = transform.parent.parent.GetComponent<VillagerCtrl>();
        Debug.LogWarning(transform.name + ": LoadVillagerCtrl", gameObject);
    }

    public override void OnClick()
    {
        this.villagerCtrl.GiveItem();
    }
}
