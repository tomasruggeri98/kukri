using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Este m�todo se llama cuando otro collider entra en el trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el collider que entra tiene la etiqueta "Player"
        if (other.CompareTag("Player"))
        {
            // Iniciar la rutina de eliminaci�n
            StartCoroutine(RemoveAfterDelay());
        }
    }

    // Coroutine para esperar un segundo antes de eliminar el objeto
    private IEnumerator RemoveAfterDelay()
    {
        // Esperar un segundo
        yield return new WaitForSeconds(1f);

        // Destruir el objeto
        Destroy(gameObject);
    }
}