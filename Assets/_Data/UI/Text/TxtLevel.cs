using UnityEngine;

public class TxtLevel : TextAbstact
{
    protected virtual void FixedUpdate()
    {
        this.LoadCount();
    }

    protected virtual void LoadCount()
    {
        this.textPro.text = PlayerCtrl.Instance.Level.GetFormatted();
    }
}
