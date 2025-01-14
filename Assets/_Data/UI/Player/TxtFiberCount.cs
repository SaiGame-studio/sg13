using UnityEngine;

public class TxtFiberCount : TxtUpdate
{
    protected override void ShowingText()
    {
        this.textPro.text = Mathf.RoundToInt(PlayerCtrl.Instance.Needs.Fiber).ToString();
    }
}
