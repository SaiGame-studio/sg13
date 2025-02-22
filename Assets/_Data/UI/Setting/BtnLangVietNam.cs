
public class BtnLangVietNam : ButttonAbstract
{
    public override void OnClick()
    {
        LanguageManager.Instance.ChangeLanguage(LanguageCode.vi);
    }
}
