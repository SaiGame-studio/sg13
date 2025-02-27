using UnityEngine;

public class UIGameOver : UIShowHide
{
    protected virtual void FixedUpdate()
    {
        this.showHide.gameObject.SetActive(!PlayerNeeds.Instance.IsAlive);
    }
    
}
