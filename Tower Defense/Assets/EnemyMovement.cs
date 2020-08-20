using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<WayPoint> path;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
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


    // Update is called once per frame
    void Update()
    {
    }
}