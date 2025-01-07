using UnityEngine;

public class PlayerNeeds : SaiSingleton<PlayerNeeds>
{
    [SerializeField] protected PlayerCtrl playerCtrl;

    [Header("Status")]
    [SerializeField] protected bool noDecay = false;
    [SerializeField] protected bool isSleeping = false;
    [SerializeField] protected bool isEathing = false;
    public bool IsEathing => isEathing;

    [Header("Needs")]
    [SerializeField] protected float maxNeeds = 100f; // Current hunger level
    [SerializeField] protected float hunger = 100f; // Current hunger level
    [SerializeField] protected float thirst = 100f; // Current thirst level
    [SerializeField] protected float fiber = 70f; // Current thirst level
    [SerializeField] protected int eatPerDay = 1;
    [SerializeField] protected int eatPerDayMax = 1;

    [Header("Decay Rates")]
    [SerializeField] protected float hungerDecayRate = 0.2f; // How fast hunger decreases (per second)
    [SerializeField] protected float thirstDecayRate = 0.3f; // How fast thirst decreases (per second)
    [SerializeField] protected float fiberDecayRate = 0.1f; // How fast thirst decreases (per second)
    [SerializeField] protected float restingDecayRate = 0.05f;

    [Header("Critical Levels")]
    [SerializeField] protected float criticalHunger = 27f; // Threshold for critical hunger
    [SerializeField] protected float criticalThirst = 27f; // Threshold for critical thirst
    [SerializeField] protected bool isAlive = true; // Player's alive state

    public bool IsAlive { get { return isAlive; } }

    private void Update()
    {
        this.UpdateStatus();
        this.Needing();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;
        this.playerCtrl = GetComponentInParent<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    protected virtual void Needing()
    {
        this.playerCtrl.Animator.SetBool("isAlive", this.isAlive);

        if (!isAlive) return;

        if (!this.noDecay) this.DecayNeeding();

        this.hunger = Mathf.Clamp(this.hunger, 0f, 100f);
        this.thirst = Mathf.Clamp(this.thirst, 0f, 100f);
        this.fiber = Mathf.Clamp(this.fiber, 0f, 100f);

        this.CheckCriticalNeeds();
    }

    protected virtual void DecayNeeding()
    {

        float hungerRate = this.hungerDecayRate;
        if (this.thirst <= 0) hungerRate *= 4;
        if (this.fiber <= 0) hungerRate *= 2;

        float thirstRate = this.thirstDecayRate;
        if (this.hunger <= 0) thirstRate *= 3;
        if (this.fiber <= 0) thirstRate *= 2;

        if (this.isSleeping)
        {
            hungerRate = this.restingDecayRate;
            thirstRate = this.restingDecayRate;
        }

        this.hunger -= hungerRate * Time.deltaTime;
        this.thirst -= thirstRate * Time.deltaTime;

        float fiberRate = this.fiberDecayRate;
        this.fiber -= fiberRate * Time.deltaTime;
    }

    public void Eat(float amount)
    {
        if (!this.isAlive) return;
        this.hunger += amount;
        this.hunger = Mathf.Clamp(this.hunger, 0f, 100f);
        //this.SetEating(true);
    }

    public bool CanEat(float amount)
    {
        if (!this.isAlive) return false;
        float newHunger = this.hunger + amount;
        return newHunger <= this.maxNeeds;
    }

    public void Sew(float amount)
    {
        if (!this.isAlive) return;
        this.fiber += amount;
        this.fiber = Mathf.Clamp(this.fiber, 0f, 100f);
    }

    public bool CanSew(float amount)
    {
        if (!isAlive) return false;
        float newFiber = this.fiber + amount;
        return newFiber <= this.maxNeeds;
    }

    public void Drink(float amount)
    {
        if (!isAlive) return;
        thirst += amount;
        thirst = Mathf.Clamp(thirst, 0f, 100f);
    }

    public bool CanDrink(float amount)
    {
        if (!isAlive) return false;
        float newThirst = this.thirst + amount;
        return newThirst <= this.maxNeeds;
    }

    protected virtual void CheckCriticalNeeds()
    {
        if (hunger <= criticalHunger) HandleCriticalHunger();
        if (thirst <= criticalThirst) HandleCriticalThirst();
        if (hunger <= 0f && thirst <= 0f) PlayerDeath();
    }

    protected virtual void HandleCriticalHunger() { }

    protected virtual void HandleCriticalThirst() { }

    protected virtual void PlayerDeath()
    {
        this.isAlive = false;
    }

    public virtual float HungerValue()
    {
        return this.hunger / this.maxNeeds;
    }

    public virtual float ThirstValue()
    {
        return this.thirst / this.maxNeeds;
    }

    public virtual float FiberValue()
    {
        return this.fiber / this.maxNeeds;
    }

    public virtual void ResetEatPerDay()
    {
        this.eatPerDay = this.eatPerDayMax;
    }

    protected virtual void UpdateStatus()
    {
        this.isSleeping = DayNightCycle.Instance.IsNight() && PlayerCtrl.Instance.Moving.IsSitting;
        if (DayNightCycle.Instance.IsNight()) this.eatPerDay = this.eatPerDayMax;
    }

    public virtual void SetEating(bool status)
    {
        this.isEathing = status;
    }

    public virtual void CheckEating()
    {
        if (this.isEathing) this.eatPerDay--;
        this.SetEating(false);
    }

    public virtual bool IsFinishEat()
    {
        return this.eatPerDay <= 0;
    }

    public virtual string FormattedEatCount()
    {
        return $"{eatPerDay}/{eatPerDayMax}";
    }

    public virtual bool IsSleeping()
    {
        return this.isSleeping;
    }
}
