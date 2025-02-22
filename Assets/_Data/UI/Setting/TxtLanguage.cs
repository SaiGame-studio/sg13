public class TxtLanguage : TxtUpdate
{
    protected override void ShowingText()
    {
        this.textPro.text = LanguageManager.T("Language");
    }
}
