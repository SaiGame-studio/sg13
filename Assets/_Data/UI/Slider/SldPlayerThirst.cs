using UnityEngine;

public class SldPlayerThirst : SliderReadOnly
{
    protected override float GetValue()
    {
        return PlayerNeeds.Instance.ThirstValue();
    }
}
