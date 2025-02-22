using UnityEditor;
using UnityEngine;

public class BtnExitGame : ButttonAbstract
{
    public override void OnClick()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
