public class SldFiberPlayer : SliderReadOnly
{
    protected override float GetValue()
    {
        return PlayerNeeds.Instance.FiberValue();
    }
}
