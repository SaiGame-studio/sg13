using UnityEngine;

public class InventoryChecker : SaiBehaviour
{
    [SerializeField] protected bool isCheckItemOverSleep = false;

    protected virtual void FixedUpdate()
    {
        this.CheckItemOverSleep();
    }

    protected virtual void CheckItemOverSleep()
    {
        if (!PlayerNeeds.Instance.IsSleeping()) return;
        if (this.isCheckItemOverSleep) return;

        this.isCheckItemOverSleep = true;
        foreach(ItemInventory itemInventory in InventoryManager.Instance.Items().Items)
        {
            if (itemInventory.ItemProfile.isFood || itemInventory.ItemProfile.isKarma)
            {
                int deductNumber = itemInventory.ItemProfile.fate;
                InventoryManager.Instance.DeductFate(deductNumber);
            }
        }
    }
}
