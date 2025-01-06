using UnityEngine;

public class PlayerNeeds : SaiSingleton<PlayerNeeds>
{
    [SerializeField] protected PlayerCtrl playerCtrl;

    [Header("Player Needs")]
    [SerializeField] protected bool noDecay = false; // Current hunger level
    [SerializeField] protected float maxNeeds = 100f; // Current hunger level
    [SerializeField] protected float hunger = 70f; // Current hunger level
    [SerializeField] protected float thirst = 70f; // Current thirst level
    [SerializeField] protected float fiber = 50f; // Current thirst level

    [Header("Decay Rates")]
    [SerializeField] protected float hungerDecayRate = 0.7f; // How fast hunger decreases (per second)
    [SerializeField] protected float thirstDecayRate = 1f; // How fast thirst decreases (per second)
    [SerializeField] protected float fiberDecayRate = 0.5f; // How fast thirst decreases (per second)

    [Header("Critical Levels")]
    [SerializeField] protected float criticalHunger = 27f; // Threshold for critical hunger
    [SerializeField] protected float criticalThirst = 27f; // Threshold for critical thirst

    [SerializeField] protected bool isAlive = true; // Player's alive state
    public bool IsAlive { get { return isAlive; } }

    private void Update()
    {
        this.Needing();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
    }

    protected virtual void LoadPlayerCtrl()
    {
        if(this.playerCtrl != null) return;
        this.playerCtrl = GetComponentInParent<PlayerCtrl>(); 
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    protected virtual void Needing()
    {
        this.playerCtrl.Animator.SetBool("isAlive", this.isAlive);

        if (!isAlive) return;

        if(!this.noDecay) this.DecayNeeding();

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
        this.hunger -= hungerRate * Time.deltaTime;

        float thirstRate = this.thirstDecayRate;
        if (this.hunger <= 0) thirstRate *= 3;
        if (this.fiber <= 0) thirstRate *= 2;
        this.thirst -= thirstRate * Time.deltaTime;

        float fiberRate = this.fiberDecayRate;
        this.fiber -= fiberRate * Time.deltaTime;
    }

    public void Eat(float amount)
    {
        if (!isAlive) return;

        hunger += amount;
        hunger = Mathf.Clamp(hunger, 0f, 100f);
        //Debug.Log($"Player ate food. Hunger restored to {hunger}.");
    }

    public bool CanEat(float amount)
    {
        if (!isAlive) return false;
        float newHunger = this.hunger + amount;
        return newHunger <= this.maxNeeds;
    }

    public void Sew(float amount)
    {
        if (!isAlive) return;

        fiber += amount;
        fiber = Mathf.Clamp(fiber, 0f, 100f);
        //Debug.Log($"Player sew fiber. Fiber restored to {hunger}.");
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
        //Debug.Log($"Player drank water. Thirst restored to {thirst}.");
    }

    public bool CanDrink(float amount)
    {
        if (!isAlive) return false;
        float newThirst = this.thirst + amount;
        return newThirst <= this.maxNeeds;
    }

    protected virtual void CheckCriticalNeeds()
    {
        if (hunger <= criticalHunger)
        {
            //Debug.LogWarning("Hunger level is critical!");
            HandleCriticalHunger();
        }

        if (thirst <= criticalThirst)
        {
            //Debug.LogWarning("Thirst level is critical!");
            HandleCriticalThirst();
        }

        // If both hunger and thirst reach 0, the player dies
        if (hunger <= 0f && thirst <= 0f)
        {
            PlayerDeath();
        }
    }

    protected virtual void HandleCriticalHunger()
    {
        // Implement effects like slower movement, health decrease, etc.
        //Debug.Log("Player is very hungry. Consider eating soon.");
    }

    protected virtual void HandleCriticalThirst()
    {
        // Implement effects like slower movement, blurred vision, etc.
        //Debug.Log("Player is very thirsty. Consider drinking water soon.");
    }

    protected virtual void PlayerDeath()
    {
        this.isAlive = false;
        //Debug.LogError("Player has died from starvation and dehydration!");
        // Trigger death logic (e.g., respawn, game over screen, etc.)
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
}
