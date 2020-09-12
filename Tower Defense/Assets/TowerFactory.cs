using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;

    Queue<Tower> towerQueue = new Queue<Tower>();

    public void AddTower(WayPoint baseWaypoint)
    {
        int towerFactoryCount = towerQueue.Count;
        if (towerFactoryCount < towerLimit)
        {
            AddNewTower(baseWaypoint);
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }
    }

    private void AddNewTower(WayPoint baseWaypoint)
    {
        var tower = Instantiate(towerPrefab, baseWaypoint.transform.position, Quaternion.identity);
        baseWaypoint.isPlaceable = false;

        // put new tower on queue
        towerQueue.Enqueue(tower);
    }

    private void MoveExistingTower(WayPoint baseWaypoint)
    {
        var oldTower = towerQueue.Dequeue();


        towerQueue.Enqueue(oldTower);
    }
}