using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseController : MonoBehaviour
{
    [System.Serializable]
    public struct DropItem
    {
        public GameObject itemPrefab; // Prefab del objeto a dropear
        public float dropChance; // Probabilidad de dropear el objeto (0.0 - 1.0)
    }

    public DropItem[] dropItems; // Array de posibles objetos a dropear

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el objeto que colision� tiene la etiqueta "Proyectil"
        if (collision.gameObject.CompareTag("Proyectil"))
        {
            // Destruir el proyectil despu�s de la colisi�n
            Destroy(collision.gameObject);

            // Dropear un objeto con cierta probabilidad
            TryDropItem();

            // Destruir el jarr�n
            Destroy(gameObject);
        }
    }

    void TryDropItem()
    {
        foreach (DropItem dropItem in dropItems)
        {
            // Generar un n�mero aleatorio entre 0 y 1
            float randomValue = Random.value;

            // Comparar el n�mero aleatorio con la probabilidad de dejar caer el objeto
            if (randomValue <= dropItem.dropChance)
            {
                // Instanciar el objeto en la posici�n del jarr�n
                Instantiate(dropItem.itemPrefab, transform.position, Quaternion.identity);
                return; // Salir despu�s de dropear un objeto
            }
        }
    }
}
