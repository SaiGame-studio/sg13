using UnityEngine;

public class UINoEat : UIShowHide
{
    protected virtual void FixedUpdate()
    {
        if(DayNightCycle.Instance.IsEatTime) this.showHide.gameObject.SetActive(false);
        else this.showHide.gameObject.SetActive(true);
    }
}
