using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseController : MonoBehaviour
{
    public GameObject dropPrefab; // Prefab del objeto a dropear
    public float dropChance = 0.5f; // Probabilidad de dropear el objeto (0.0 - 1.0)

    void OnCollisionEnter2D(Collision2D collision)
    {
        

        // Verificar si el objeto que colision� tiene la etiqueta "Proyectil"
        if (collision.gameObject.CompareTag("Proyectil"))
        {
            

            // Destruir el proyectil despu�s de la colisi�n
            Destroy(collision.gameObject);

            // Dropear el objeto con cierta probabilidad
            TryDropItem();

            // Destruir el jarr�n
            Destroy(gameObject);
        }
    }

    void TryDropItem()
    {
        // Generar un n�mero aleatorio entre 0 y 1
        float randomValue = Random.value;
        

        // Comparar el n�mero aleatorio con la probabilidad de dejar caer el objeto
        if (randomValue <= dropChance)
        {
            
            // Instanciar el objeto en la posici�n del jarr�n
            Instantiate(dropPrefab, transform.position, Quaternion.identity);
        }
    }
}