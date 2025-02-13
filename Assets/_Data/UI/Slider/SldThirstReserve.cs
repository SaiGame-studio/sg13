public class SldThirstReserve : SliderReadOnly
{
    protected override float GetValue()
    {
        return PlayerNeeds.Instance.ThirstReserve();
    }
}
