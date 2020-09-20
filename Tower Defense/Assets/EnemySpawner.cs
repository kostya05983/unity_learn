using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 120f)] [SerializeField] float secondsBetweenSpawns = 2f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform enemyParentTransform;
    [SerializeField] Text scoreText;
    [SerializeField] AudioClip spawnedEnemySFX;

    private double score = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
        StartCoroutine(RepeatedSpawnEnemies());
    }

    IEnumerator RepeatedSpawnEnemies()
    {
        while (true)
        {
            IncreaseScore();
            GetComponent<AudioSource>().PlayOneShot(spawnedEnemySFX);
            var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemy.transform.parent = enemyParentTransform;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }

    private void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}