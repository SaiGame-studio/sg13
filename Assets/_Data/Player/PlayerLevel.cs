using UnityEngine;

public class PlayerLevel : LevelByItem
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.maxLevel = 13;
    }

    public virtual string GetFormatted()
    {
        int exp = this.playerExp?.itemCount ?? 0;
        string expString = NumberFormatter.FormatNumber(exp);

        float nextExp = Mathf.Ceil(this.nextLevelExp);
        string nextExpString = NumberFormatter.FormatNumber(nextExp);

        string label = Language.T("Lvl");
        return $"{label} {this.currentLevel}: {expString}/{nextExpString}";
    }
}
