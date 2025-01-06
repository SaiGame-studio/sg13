using System.Collections.Generic;
using UnityEngine;

public class FiberCtrl : OnRoadCtrl
{
    [Header("Fiber")]
    [SerializeField] protected List<ItemCode> giveItems = new()
        {
            ItemCode.Fiber1,
            ItemCode.Fiber2,
            ItemCode.Fiber3,
            ItemCode.Fiber4,
            ItemCode.Fiber5,
            ItemCode.Fiber6,
            ItemCode.Fiber7,
            ItemCode.Skin1,
            ItemCode.Skin2,
            ItemCode.Skin3,
        };

    public override string GetName()
    {
        return OnRoadCode.Fiber.ToString();
    }

    protected override ItemCode RandomItem()
    {
        return this.inventoryManager.RandomItem(this.giveItems);
    }
}
