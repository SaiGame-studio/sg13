using UnityEngine;

public class GameManager : SaiSingleton<GameManager>
{
    [SerializeField] protected int ScreenWidth;
    [SerializeField] protected int ScreenHeight;
    [SerializeField] protected float ScreenAspectRatio;
    [SerializeField] protected CanvasManager canvasManager;
    [SerializeField] protected bool isPause = false;
    public bool IsPause => isPause;

    protected override void Awake()
    {
        base.Awake();
        //DontDestroyOnLoad(gameObject);
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

    public virtual void Pause()
    {
        Time.timeScale = 0.0f;
    }

    public virtual void UnPause()
    {
        Time.timeScale = 1.0f;
    }

    public virtual void TogglePause()
    {
        if (this.isPause) this.UnPause();
        else this.Pause();
        this.isPause = !this.isPause;
    }
}
