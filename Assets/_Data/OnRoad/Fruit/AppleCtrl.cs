using UnityEngine;

public class AppleCtrl : OnRoadCtrl
{
    //[Header("Apple")]

    public override string GetName()
    {
        return OnRoadCode.Apple.ToString();
    }

    protected override void ResetValue()
    {
        base.ResetValue();

        this.itemsGive = new()
        {
            ItemCode.Apple1,
            ItemCode.Apple2,
            ItemCode.Apple3,
        };
    }
}
