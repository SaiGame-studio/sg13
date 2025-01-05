using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : LevelByItem
{
    protected override void ResetValue()
    {
        base.ResetValue();
        this.maxLevel = 13;
    }
}
