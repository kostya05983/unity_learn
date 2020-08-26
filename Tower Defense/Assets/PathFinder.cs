using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] WayPoint startWaypoint, endWaypoint;
    Dictionary<Vector2Int, WayPoint> grid = new Dictionary<Vector2Int, WayPoint>();

    private Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    // Start is called before the first frame update
    void Start()
    {
        LoadBlocks();
        ColorStartAndEnd();
        ExploreNeighbours();
    }

    private void ExploreNeighbours()
    {
        foreach (var direction in directions)
        {
            Vector2Int explorationCoordinates = startWaypoint.GetGridPos() + direction;
            print("Exploring" + explorationCoordinates);
            if (grid.ContainsKey(explorationCoordinates)) grid[explorationCoordinates].SetTopColor(Color.blue);
        }
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<WayPoint>();
        foreach (var waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            if (grid.ContainsKey(gridPos))
            {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
            }
        }
    }
}