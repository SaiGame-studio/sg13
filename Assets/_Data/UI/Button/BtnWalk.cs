using UnityEngine;

public class BtnWalk : ButttonAbstract
{
    public override void OnClick()
    {
        PlayerCtrl.Instance.Moving.ToggleWalking();
    }
}
