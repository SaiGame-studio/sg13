using UnityEngine;

public class TxtDayTime : TextAbstact
{
    protected virtual void FixedUpdate()
    {
        this.LoadCount();
    }

    protected virtual void LoadCount()
    {
        string count = "0 00:00";
        count = DayNightCycle.Instance.GetFormattedTime();
        this.textPro.text = count;
    }
}
