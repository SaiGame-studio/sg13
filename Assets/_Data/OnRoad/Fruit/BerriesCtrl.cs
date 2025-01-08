using UnityEngine;

public class BerriesCtrl : OnRoadCtrl
{
    //[Header("Berries")]

    public override string GetName()
    {
        return OnRoadCode.Berry.ToString();
    }

    protected override void ResetValue()
    {
        base.ResetValue();

        this.itemsGive = new()
        {
            ItemCode.Berry1,
            ItemCode.Berry2,
        };
    }
}
