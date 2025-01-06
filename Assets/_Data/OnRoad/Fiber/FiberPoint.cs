using System.Collections.Generic;
using UnityEngine;

public class FiberPoint : OnRoadPoint
{
    protected override List<string> GetRandomList()
    {
        return new()
        {
            OnRoadCode.Fiber.ToString(),
        };
    }
}
