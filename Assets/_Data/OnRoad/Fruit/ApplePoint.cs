using System.Collections.Generic;
using UnityEngine;

public class ApplePoint : OnRoadPoint
{
    protected override List<string> GetRandomList()
    {
        return new()
        {
            OnRoadCode.Apple.ToString(),
        };
    }
}
