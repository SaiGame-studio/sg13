public class SldFoodPlayer : SliderReadOnly
{
    protected override float GetValue()
    {
        return PlayerNeeds.Instance.HungerValue();
    }
}
