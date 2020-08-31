using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PathFinder pathFinder = FindObjectOfType<PathFinder>();
        var path = pathFinder.GetPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<WayPoint> path)
    {
        print("Starting patrol...");
        foreach (var block in path)
        {
            transform.position = block.transform.position;
            print("Visiting: " + block);
            yield return new WaitForSeconds(1f);
        }

        print("Ending patrol");
    }
}