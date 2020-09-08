﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float secondsBetweenSpawns = 2f;
    [SerializeField] EnemyMovement enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RepeatedSpawnEnemies());
    }

    IEnumerator RepeatedSpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(secondsBetweenSpawns);    
        }
    }
}