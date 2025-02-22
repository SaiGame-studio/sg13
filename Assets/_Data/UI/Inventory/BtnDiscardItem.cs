using UnityEngine;

public class BtnDiscardItem : ButttonAbstract
{
    public override void OnClick()
    {
        InventoryManager.Instance.DiscardChoosedItem();
    }
}
