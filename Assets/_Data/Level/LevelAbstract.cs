using UnityEngine;

public abstract class LevelAbstract : SaiBehaviour
{
    [Range(1,13)]
    [SerializeField] protected int currentLevel = 1;
    public int CurrentLevel => currentLevel;

    [SerializeField] protected int maxLevel = 100;
    [SerializeField] protected float nextLevelExp;

    protected abstract int GetCurrentExp();
    protected abstract bool DeductExp(int exp);

    protected virtual void FixedUpdate()
    {
        this.Leveling();
    }

    protected virtual void Leveling()
    {
        if (this.currentLevel >= this.maxLevel) return;
        if (this.GetCurrentExp() < this.GetNextLevelExp()) return;
        if (!this.DeductExp((int)this.GetNextLevelExp())) return;
        this.currentLevel++;
    }

    public virtual float GetNextLevelExp()
    {
        float growthFactor = 1.5f;
        int baseExp = 100;
        int nextLevel = this.currentLevel - 1;
        if (nextLevel <= 0) nextLevel = 1;
        this.nextLevelExp = Mathf.FloorToInt(baseExp * Mathf.Pow(growthFactor, nextLevel));
        return this.nextLevelExp;
    }

    public virtual void SetLevel(int level)
    {
        this.currentLevel = level;
        this.CheckLevelLimit();
    }

    protected virtual void CheckLevelLimit()
    {
        if (this.currentLevel > this.maxLevel) this.currentLevel = this.maxLevel;
    }
}
