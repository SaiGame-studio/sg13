
public class BtnToogleSetting : ButttonAbstract
{
    public override void OnClick()
    {
        UISetting.Instance.Toggle();
    }
}
