
public class BtnPauseGame : ButttonAbstract
{
    public override void OnClick()
    {
        GameManager.Instance.TogglePause();
    }
}
