using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Prefab del enemigo que se va a generar
    public Transform[] spawnPoints; // Puntos de generaci�n de enemigos
    public float minSpawnTime = 1f; // Tiempo m�nimo de generaci�n
    public float maxSpawnTime = 3f; // Tiempo m�ximo de generaci�n
    public float maxSpawnSpeedup = 1f; // Velocidad m�xima de aumento del spawn
    public Transform player; // Referencia al jugador

    private float currentSpawnTime; // Tiempo actual de generaci�n
    private float spawnSpeedupIncrement; // Incremento de la velocidad de spawn

    private void Start()
    {
        // Iniciar la generaci�n de enemigos
        StartSpawning();
    }

    void StartSpawning()
    {
        // Calcular el incremento de la velocidad de spawn
        spawnSpeedupIncrement = (maxSpawnTime - minSpawnTime) / maxSpawnSpeedup;

        // Iniciar la corrutina de spawn de enemigos
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // Esperar un tiempo aleatorio entre minSpawnTime y currentSpawnTime
            float spawnTime = Random.Range(minSpawnTime, currentSpawnTime);
            yield return new WaitForSeconds(spawnTime);

            // Generar un enemigo en uno de los puntos de generaci�n aleatorios
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            GameObject enemy = Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);

            // Pasar la referencia del jugador al enemigo generado
            EnemyChaser enemyScript = enemy.GetComponent<EnemyChaser>();
            if (enemyScript != null)
            {
                enemyScript.SetPlayer(player);
            }

            // Aumentar gradualmente la velocidad de spawn hasta el m�ximo
            if (currentSpawnTime > maxSpawnTime)
            {
                currentSpawnTime -= spawnSpeedupIncrement;
            }
            else
            {
                currentSpawnTime = maxSpawnTime;
            }
        }
    }
}
