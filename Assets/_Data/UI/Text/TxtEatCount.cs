using UnityEngine;

public class TxtEatCount : TextAbstact
{
    protected virtual void FixedUpdate()
    {
        this.LoadCount();
    }

    protected virtual void LoadCount()
    {
        this.textPro.text = PlayerNeeds.Instance.FormattedEatCount();
    }
}
