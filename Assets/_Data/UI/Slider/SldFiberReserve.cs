public class SldFiberReserve : SliderReadOnly
{
    protected override float GetValue()
    {
        return PlayerNeeds.Instance.FiberReserve();
    }
}
