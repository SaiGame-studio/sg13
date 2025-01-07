using UnityEngine;

public class TxtDayTime : TextAbstact
{
    protected virtual void FixedUpdate()
    {
        this.LoadCount();
    }

    protected virtual void LoadCount()
    {
        this.textPro.text = DayNightCycle.Instance.GetFormattedTime();
    }
}
