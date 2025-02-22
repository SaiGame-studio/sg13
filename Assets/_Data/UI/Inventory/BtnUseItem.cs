using UnityEngine;

public class BtnUseItem : ButttonAbstract
{
    public override void OnClick()
    {
        InventoryManager.Instance.UseChoosedItem();
    }
}
