using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISetting : SaiSingleton<UISetting>
{
    protected bool isShow = true;
    protected bool IsShow => isShow;
    [SerializeField] protected Transform showHide;

    protected override void Start()
    {
        base.Start();
        this.Hide();
    }

    protected virtual void FixedUpdate()
    {
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadShowHide();
    }

    protected virtual void LoadShowHide()
    {
        if (this.showHide != null) return;
        this.showHide = transform.Find("ShowHide");
        Debug.Log(transform.name + ": LoadShowHide", gameObject);
    }

    public virtual void Show()
    {
        this.isShow = true;
        this.showHide.gameObject.SetActive(this.isShow);
        //GameManager.Instance.Pause();
    }

    public virtual void Hide()
    {
        this.showHide.gameObject.SetActive(false);
        this.isShow = false;
        //GameManager.Instance.UnPause();
    }

    public virtual void Toggle()
    {
        if (this.isShow) this.Hide();
        else this.Show();
    }
}