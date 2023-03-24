using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnEnemiesRange = 100.0f;
    public readonly float spawnEnemiesInterval = 5.0f;
    public GameObject enemy;

    float enemiesSpawnCoolDown;


    // Start is called before the first frame update
    void Start()
    {
        enemiesSpawnCoolDown = spawnEnemiesInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesSpawnCoolDown <= 0) SpawnEnemies((int)Mathf.Ceil(Time.time / 3));
        else enemiesSpawnCoolDown -= Time.deltaTime;
    }

    void SpawnEnemies(int num)
    {
        enemiesSpawnCoolDown = spawnEnemiesInterval;

        for (int i = 0; i < num; ++i)
        {
            Vector3 location = transform.position + new Vector3(Random.Range(-spawnEnemiesRange, spawnEnemiesRange),
            Random.Range(-spawnEnemiesRange, spawnEnemiesRange), 0);

            var bot = Instantiate(enemy, location, enemy.transform.rotation);
            bot.SetActive(true);
        }
    }
}
