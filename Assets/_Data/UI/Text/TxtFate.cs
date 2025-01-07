using UnityEngine;

public class TxtFate : TextAbstact
{
    protected virtual void FixedUpdate()
    {
        this.LoadCount();
    }

    protected virtual void LoadCount()
    {
        if(PlayerCtrl.Instance.GetFate() == null) return;

        this.textPro.text = PlayerCtrl.Instance.GetFate().itemCount.ToString();
    }
}
