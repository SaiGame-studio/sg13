using UnityEngine;

public class SldPlayerHunger : SliderReadOnly
{
    protected override float GetValue()
    {
        return PlayerNeeds.Instance.HungerValue();
    }
}
