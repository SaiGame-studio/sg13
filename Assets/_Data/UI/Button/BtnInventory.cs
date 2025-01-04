using UnityEngine;

public class BtnInventory : ButttonAbstract
{
    public override void OnClick()
    {
        UIInventory.Instance.Toggle();
    }
}
