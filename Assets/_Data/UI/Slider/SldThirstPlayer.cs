public class SldThirstPlayer : SliderReadOnly
{
    protected override float GetValue()
    {
        return PlayerNeeds.Instance.ThirstValue();
    }
}
