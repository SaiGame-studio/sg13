public class TxtPauseGame : TxtUpdate
{
    protected override void ShowingText()
    {
        string text;
        if (GameManager.Instance.IsPause) text = LanguageManager.T("Unpause");
        else text = LanguageManager.T("Pause");

        this.textPro.text = text;
    }
}
