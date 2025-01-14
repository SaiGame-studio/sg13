
using UnityEngine;

public class TxtHungerCount : TxtUpdate
{
    protected override void ShowingText()
    {
        this.textPro.text = Mathf.RoundToInt(PlayerCtrl.Instance.Needs.Hunger).ToString();
    }
}
