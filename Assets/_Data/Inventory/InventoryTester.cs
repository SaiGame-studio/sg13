using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class InventoryTester : SaiBehaviour
{
    protected override void Start()
    {
        base.Start();
        this.AddTestItems(ItemCode.Fate, 1);
        this.AddTestItems(ItemCode.Karma, 1);

        this.AddTestItems(ItemCode.Water, 1);
        this.AddTestItems(ItemCode.Banana, 1);
        this.AddTestItems(ItemCode.Gold, 1);
        this.AddTestItems(ItemCode.Meat, 1);
    }

    [ProButton]
    public virtual void AddTestItems(ItemCode itemCode, int count)
    {
        for (int i = 0; i < count; i++)
        {
            InventoryManager.Instance.AddItem(itemCode, 1);
        }
    }

    [ProButton]
    public virtual void RemoveTestItems(ItemCode itemCode, int count)
    {
        for (int i = 0; i < count; i++)
        {
            InventoryManager.Instance.RemoveItem(itemCode, 1);
        }
    }
}
