using UnityEngine;

public class BtnChooseScreenMode : ButttonAbstract
{
    public override void OnClick()
    {
        string buttonName = transform.name.Replace("Btn","");
        ScreenModeManager.Instance.SetScreenMode(buttonName);
    }
}
