using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower towerPrefab;
    [SerializeField] Transform towerParentTransform;

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
        tower.transform.parent = towerParentTransform.transform;
        tower.baseWaypoit = baseWaypoint;
        baseWaypoint.isPlaceable = false;

        // put new tower on que ue
        towerQueue.Enqueue(tower);
    }

    private void MoveExistingTower(WayPoint baseWaypoint)
    {
        var oldTower = towerQueue.Dequeue();
        oldTower.baseWaypoit.isPlaceable = true;
        baseWaypoint.isPlaceable = false;
        oldTower.baseWaypoit = baseWaypoint;

        oldTower.transform.position = baseWaypoint.transform.position;

        towerQueue.Enqueue(oldTower);
    }
}