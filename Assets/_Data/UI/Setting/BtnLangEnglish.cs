
public class BtnLangEnglish : ButttonAbstract
{
    public override void OnClick()
    {
        LanguageManager.Instance.ChangeLanguage(LanguageCode.en);
    }
}
