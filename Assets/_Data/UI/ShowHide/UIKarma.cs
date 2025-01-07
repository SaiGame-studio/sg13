using UnityEngine;

public class UIKarma : UIShowHide
{
    protected virtual void FixedUpdate()
    {
        this.showHide.gameObject.SetActive(PlayerCtrl.Instance.IsHasKarma);
    }
}
