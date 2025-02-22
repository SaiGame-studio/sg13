
using UnityEngine;

public class BtnOpenDiscord : ButttonAbstract
{

    public override void OnClick()
    {
        Application.OpenURL("https://discord.gg/KK5NBAxVnW");
    }
}
