using System.Collections.Generic;
using UnityEngine;

public class BerriesPoint : OnRoadPoint
{
    protected override List<string> GetRandomList()
    {
        return new()
        {
            OnRoadCode.Berry.ToString(),
        };
    }
}
