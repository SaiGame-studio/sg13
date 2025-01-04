using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnCloseInventory : ButttonAbstract
{
    public virtual void CloseInventoryUI()
    {
        UIInventory.Instance.Hide();
    }

    public override void OnClick()
    {
        this.CloseInventoryUI();
    }
}
