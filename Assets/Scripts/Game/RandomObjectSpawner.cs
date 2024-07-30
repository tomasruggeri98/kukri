using System.Collections;
using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // Lista de objetos que se pueden generar
    public int initialObjectCount = 100; // Cantidad de objetos a generar al inicio
    public Vector2 spawnAreaSize = new Vector2(10f, 10f); // Tamaño del área donde se generarán los objetos

    void Start()
    {
        // Generar objetos inmediatamente al iniciar el juego
        SpawnInitialObjects();
    }

    void SpawnInitialObjects()
    {
        for (int i = 0; i < initialObjectCount; i++)
        {
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        // Verifica que haya al menos un objeto en la lista para generar
        if (objectsToSpawn.Length == 0)
        {
            Debug.LogWarning("No hay objetos para generar.");
            return;
        }

        // Elige un objeto aleatorio de la lista
        GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

        // Calcula una posición aleatoria dentro del área de generación
        float spawnX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
        float spawnY = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
        Vector2 spawnPosition = new Vector2(spawnX, spawnY) + (Vector2)transform.position;

        // Genera el objeto en la posición aleatoria
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}
