using UnityEngine;

public class CanvasManager : SaiBehaviour
{
    [SerializeField] protected Transform portraitCanvas;
    [SerializeField] protected Transform landscapeCanvas;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCanvas();
    }

    protected virtual void LoadCanvas()
    {
        if (this.portraitCanvas != null && this.landscapeCanvas != null) return;

        this.portraitCanvas = transform.Find("PortraitCanvas");
        this.landscapeCanvas = transform.Find("LandscapeCanvas");

        this.portraitCanvas.gameObject.SetActive(false);
        this.landscapeCanvas.gameObject.SetActive(false);
        Debug.LogWarning(transform.name + ": LoadCanvas", gameObject);
    }

    public virtual void ShowCanvas(bool isPortrait)
    {
        if(isPortrait) this.portraitCanvas.gameObject.SetActive(true);
        else this.landscapeCanvas.gameObject.SetActive(true);
    }

}
