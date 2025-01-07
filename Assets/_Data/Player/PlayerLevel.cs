using UnityEngine;

public class PlayerLevel : LevelByItem
{
    [SerializeField] protected float maxExp = 89986f;

    protected override void ResetValue()
    {
        base.ResetValue();
        this.currentLevel = 0;
        this.maxLevel = 13;
    }

    public virtual string GetFormatted()
    {
        float exp = Mathf.Ceil(this.playerExp?.itemCount ?? 0);
        exp = this.maxExp - exp;
        if (exp <= 0) exp = 0;
        string expString = NumberFormatter.FormatNumber(exp);
        if (expString == "0") expString = "00";

        float nextExp = Mathf.Ceil(this.nextLevelExp);
        nextExp = this.maxExp - nextExp;
        if (nextExp <= 0) nextExp = 0;
        string nextExpString = NumberFormatter.FormatNumber(nextExp);
        if (nextExpString == "0") nextExpString = "00";

        int currentLevel = this.maxLevel - this.currentLevel;

        string label = Language.T("Lvl");
        return $"{label} {currentLevel}: {expString}/{nextExpString}";
    }
}
