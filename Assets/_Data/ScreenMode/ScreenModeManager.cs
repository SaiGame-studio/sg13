using UnityEngine;
using System;

public class ScreenModeManager : SaiSingleton<ScreenModeManager>
{
    public enum ScreenMode
    {
        FullScreen,
        Borderless,
        Windowed_1920x1080,
        Windowed_1600x900,
        Windowed_1366x768,
        Windowed_1280x720
    }

    public void SetScreenMode(ScreenMode mode)
    {
        switch (mode)
        {
            case ScreenMode.FullScreen:
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.ExclusiveFullScreen);
                break;
            case ScreenMode.Borderless:
                Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.FullScreenWindow);
                break;
            case ScreenMode.Windowed_1920x1080:
                Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
                break;
            case ScreenMode.Windowed_1600x900:
                Screen.SetResolution(1600, 900, FullScreenMode.Windowed);
                break;
            case ScreenMode.Windowed_1366x768:
                Screen.SetResolution(1366, 768, FullScreenMode.Windowed);
                break;
            case ScreenMode.Windowed_1280x720:
                Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
                break;
        }
    }

    public void SetScreenMode(string modeName)
    {
        if (Enum.TryParse(modeName, out ScreenMode mode))
        {
            SetScreenMode(mode);
        }
        else
        {
            Debug.LogError($"Invalid screen mode: {modeName}");
        }
    }
}
