using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo que se va a generar
    public Transform[] spawnPoints; // Puntos de generación de enemigos
    public float minSpawnTime = 1f; // Tiempo mínimo de generación
    public float maxSpawnTime = 3f; // Tiempo máximo de generación
    public Transform player; // Referencia al jugador

    private void Start()
    {
        StartSpawning();
    }

    void StartSpawning()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // Esperar un tiempo aleatorio entre minSpawnTime y maxSpawnTime
            float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(spawnTime);

            // Generar un enemigo en uno de los puntos de generación aleatorios
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

            // Pasar la referencia del jugador al enemigo generado
            EnemyChaser enemyScript = enemy.GetComponent<EnemyChaser>();
            if (enemyScript != null)
            {
                enemyScript.SetPlayer(player);
            }
        }
    }
}
