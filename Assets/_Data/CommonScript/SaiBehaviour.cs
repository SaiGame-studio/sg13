using UnityEngine;

public class SaiBehaviour : MonoBehaviour
{
    
    protected virtual void Start()
    {
        //
    }

    protected virtual void Awake()
    {
        this.LoadComponents();
    }

    protected virtual void Reset()
    {
        this.ResetValue();
        this.LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        //For override
    }

    protected virtual void ResetValue()
    {
        //For override
    }

    public virtual void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
