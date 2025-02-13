using UnityEngine;

public class TxtThirstCount : TxtUpdate
{
    protected override void ShowingText()
    {
        this.textPro.text = Mathf.RoundToInt(PlayerCtrl.Instance.Needs.Thirst).ToString();
    }
}
