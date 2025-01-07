using UnityEngine;

public class GameManager : SaiSingleton<GameManager>
{
    [SerializeField] protected int ScreenWidth;
    [SerializeField] protected int ScreenHeight;
    [SerializeField] protected float ScreenAspectRatio;
    [SerializeField] protected CanvasManager canvasManager;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        this.GetScreenSize();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadCanvasManager();
    }

    protected virtual void LoadCanvasManager()
    {
        if (this.canvasManager != null) return;
        this.canvasManager = GameManager.FindAnyObjectByType<CanvasManager>();  
        Debug.LogWarning(transform.name + ": LoadCanvasManager", gameObject);
    }

    private void GetScreenSize()
    {
        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;
        ScreenAspectRatio = (float)ScreenWidth / ScreenHeight;

        this.canvasManager.ShowCanvas(this.IsPortrait());
        //Debug.Log($"Screen Size: {ScreenWidth}x{ScreenHeight}");
        //Debug.Log($"Aspect Ratio: {ScreenAspectRatio:F2}");
    }

    public virtual bool IsPortrait()
    {
        return this.ScreenAspectRatio < 1;
    }
}
