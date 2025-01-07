using UnityEngine;

public class UIVersion : TextAbstact
{

    protected override void Start()
    {
        this.textPro.text = "v" + Application.version;
    }
}
