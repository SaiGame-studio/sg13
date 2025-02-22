using System;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class InventoryTester : SaiBehaviour
{
    protected override void Start()
    {
        base.Start();
        this.AddAllItems();
    }

    protected virtual void AddAllItems()
    {
        foreach (ItemCode item in Enum.GetValues(typeof(ItemCode)))
        {
            if(item == ItemCode.NoItem) continue;
            if(item == ItemCode.Fate) continue;
            if(item == ItemCode.Karma) continue;
            this.AddTestItems(item, 1);
        }
    }

    [ProButton]
    public virtual void AddTestItems(ItemCode itemCode, int count)
    {
        for (int i = 0; i < count; i++)
        {
            //Debug.LogWarning("AddTestItems: " + itemCode);
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
        PlayerCtrl.Instance.Level.SetLevel(0);
    }
}
