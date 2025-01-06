using UnityEngine;

[CreateAssetMenu(fileName = "ItemProfile", menuName = "ScriptableObjects/ItemProfile", order = 1)]
public class UIImages : ScriptableObject
{
    public InvCodeName invCodeName;
    public ItemCode itemCode;
    public string itemName;
    public bool canNegative = false;
    public bool isStackable = false;
    public bool isKarma = false;
    public bool isInstanceKarma = false;
    public bool useable = false;
    public int fate = 1;
    public int hunger = 0;
    public int thirst = 0;
    public int fiber = 0;
    public Sprite image;

    protected virtual void Reset()
    {
        this.ResetValue();
    }

    protected virtual void ResetValue()
    {
        this.AutoLoadItemCode();
        this.AutoLoadItemName();
    }

    protected virtual void AutoLoadItemCode()
    {
        string className = this.GetType().Name;
        Debug.Log("className: " + className);
        this.itemCode = ItemCodeParse.Parse("Item1");
    }

    protected virtual void AutoLoadItemName()
    {
        Debug.Log("name: " + this.name);
        this.itemName = "Item1";
    }
}