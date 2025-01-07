using UnityEngine;

public class UISleeps : UIShowHide
{

    protected virtual void FixedUpdate()
    {
        if(PlayerNeeds.Instance.IsSleeping()) this.showHide.gameObject.SetActive(true);
        else this.showHide.gameObject.SetActive(false);
    }
}
