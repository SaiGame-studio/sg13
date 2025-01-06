using UnityEngine;

public class PlayerNeedsManager : SaiSingleton<PlayerNeedsManager>  
{
    [Header("Player Needs")]
    [SerializeField] protected float maxNeeds = 100f; // Current hunger level
    [SerializeField] protected float hunger = 100f; // Current hunger level
    [SerializeField] protected float thirst = 100f; // Current thirst level

    [Header("Decay Rates")]
    [SerializeField] protected float hungerDecayRate = 0.7f; // How fast hunger decreases (per second)
    [SerializeField] protected float thirstDecayRate = 1f; // How fast thirst decreases (per second)

    [Header("Critical Levels")]
    [SerializeField] protected float criticalHunger = 27f; // Threshold for critical hunger
    [SerializeField] protected float criticalThirst = 27f; // Threshold for critical thirst

    [SerializeField] protected bool isAlive = true; // Player's alive state

    private void Update()
    {
        if (!isAlive) return;

        // Decrease hunger and thirst over time
        hunger -= hungerDecayRate * Time.deltaTime;
        thirst -= thirstDecayRate * Time.deltaTime;

        // Clamp hunger and thirst to prevent negative values
        hunger = Mathf.Clamp(hunger, 0f, 100f);
        thirst = Mathf.Clamp(thirst, 0f, 100f);

        // Check for critical levels
        CheckCriticalNeeds();
    }

    /// <summary>
    /// Feed the player to restore hunger.
    /// </summary>
    /// <param name="amount">Amount of hunger to restore.</param>
    public void Eat(float amount)
    {
        if (!isAlive) return;

        hunger += amount;
        hunger = Mathf.Clamp(hunger, 0f, 100f);
        Debug.Log($"Player ate food. Hunger restored to {hunger}.");
    }

    public bool CanEat(float amount)
    {
        if (!isAlive) return false;
        float newHunger = this.hunger + amount;
        return newHunger <= this.maxNeeds;
    }

    /// <summary>
    /// Give the player water to restore thirst.
    /// </summary>
    /// <param name="amount">Amount of thirst to restore.</param>
    public void Drink(float amount)
    {
        if (!isAlive) return;

        thirst += amount;
        thirst = Mathf.Clamp(thirst, 0f, 100f);
        Debug.Log($"Player drank water. Thirst restored to {thirst}.");
    }

    public bool CanDrink(float amount)
    {
        if (!isAlive) return false;
        float newThirst = this.thirst + amount;
        return newThirst <= this.maxNeeds;
    }

    /// <summary>
    /// Check if hunger or thirst levels are critical and trigger effects if necessary.
    /// </summary>
    private void CheckCriticalNeeds()
    {
        if (hunger <= criticalHunger)
        {
            Debug.LogWarning("Hunger level is critical!");
            HandleCriticalHunger();
        }

        if (thirst <= criticalThirst)
        {
            Debug.LogWarning("Thirst level is critical!");
            HandleCriticalThirst();
        }

        // If both hunger and thirst reach 0, the player dies
        if (hunger <= 0f && thirst <= 0f)
        {
            PlayerDeath();
        }
    }

    /// <summary>
    /// Handles the effects of critical hunger.
    /// </summary>
    private void HandleCriticalHunger()
    {
        // Implement effects like slower movement, health decrease, etc.
        Debug.Log("Player is very hungry. Consider eating soon.");
    }

    /// <summary>
    /// Handles the effects of critical thirst.
    /// </summary>
    private void HandleCriticalThirst()
    {
        // Implement effects like slower movement, blurred vision, etc.
        Debug.Log("Player is very thirsty. Consider drinking water soon.");
    }

    /// <summary>
    /// Handles the player's death when hunger and thirst reach 0.
    /// </summary>
    private void PlayerDeath()
    {
        isAlive = false;
        Debug.LogError("Player has died from starvation and dehydration!");
        // Trigger death logic (e.g., respawn, game over screen, etc.)
    }
}
