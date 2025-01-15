using UnityEngine;

public class FiberCtrl : OnRoadCtrl
{
    //[Header("Fiber")]

    public override string GetName()
    {
        return OnRoadCode.Fiber.ToString();
    }

    protected override void ResetValue()
    {
        base.ResetValue();

        this.itemsGive = new()
        {
            ItemCode.Fiber1,
            ItemCode.Fiber2,
            ItemCode.Fiber3,
            ItemCode.Fiber4,
            ItemCode.Fiber5,
            ItemCode.Fiber6,
            ItemCode.Fiber7,
            ItemCode.Fiber8,
            ItemCode.Fiber9,
            ItemCode.Skin1,
        };
    }
}
