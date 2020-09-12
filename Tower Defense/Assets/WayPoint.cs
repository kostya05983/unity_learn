using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WayPoint : MonoBehaviour
{
    public bool IsExplored = false;
    public WayPoint ExploredFrom;
    private Vector2Int gridPos;
    public bool isPlaceable = true;


    const int gridSize = 10;

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
        );
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && isPlaceable)
        {
            print(gameObject.name + "I'm here motherfucker'");
        }
    }
}