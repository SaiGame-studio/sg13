public class SldFoodReserve : SliderReadOnly
{
    protected override float GetValue()
    {
        return PlayerNeeds.Instance.HungerReserve();
    }
}
