using UnityEngine;

public class SldPlayerFiber : SliderReadOnly
{
    protected override float GetValue()
    {
        return PlayerNeeds.Instance.FiberValue();
    }
}
