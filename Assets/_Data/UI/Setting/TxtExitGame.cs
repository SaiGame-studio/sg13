public class TxtExitGame : TxtUpdate
{
    protected override void ShowingText()
    {
        this.textPro.text = LanguageManager.T("Exit Game");
    }
}
