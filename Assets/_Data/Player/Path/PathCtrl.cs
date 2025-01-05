using System.Collections.Generic;
using UnityEngine;

public class PathCtrl : SaiBehaviour
{
    [SerializeField] protected List<Point> points;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPoints();
    }

    public virtual void LoadPoints()
    {
        if (this.points.Count > 0) return;
        foreach (Transform child in transform)
        {
            Point point = child.GetComponent<Point>();
            this.points.Add(point);
        }
        this.UpdatePointsName();
        Debug.Log(transform.name + ": LoadPoints", gameObject);
    }

    protected virtual void UpdatePointsName()
    {
        int indexer = 0;
        foreach (Point point in this.points)
        {
            point.name = "point_" + indexer;
            indexer++;
        }
    }

    public virtual Point GetPoint(int index)
    {
        return this.points[index];
    }
}
