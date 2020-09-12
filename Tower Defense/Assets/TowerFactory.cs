using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;

    public void AddTower(WayPoint baseWaypoint)
    {
        var towers = FindObjectsOfType<Tower>();
        int towerFactoryCount = towers.Length;
        if (towerFactoryCount < towerLimit)
        {
            AddNewTower(baseWaypoint);
        }
        else
        {
            MoveExistingTower();
        }
    }

    private void AddNewTower(WayPoint baseWaypoint)
    {
        Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        baseWaypoint.isPlaceable = false;
    }

    private static void MoveExistingTower()
    {
        print("Max towers reached");
    }
}