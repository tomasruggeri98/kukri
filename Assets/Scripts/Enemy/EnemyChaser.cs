using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento del enemigo

    private Transform player; // Referencia al jugador
    private Vector2 lastDirection; // �ltima direcci�n en la que se movi� el enemigo

    void Start()
    {
        // Buscar autom�ticamente al jugador al inicio
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("No se pudo encontrar al jugador. Aseg�rate de que tiene el tag 'Player'.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Calcular la direcci�n hacia el jugador
            Vector2 direction = (player.position - transform.position).normalized;

            // Mover al enemigo hacia el jugador
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Si la direcci�n ha cambiado, girar el sprite del enemigo
            if (direction != lastDirection)
            {
                // Girar el sprite seg�n la direcci�n en la que se est� moviendo
                if (direction.x > 0)
                {
                    // Mirando a la derecha
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else if (direction.x < 0)
                {
                    // Mirando a la izquierda
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }

                // Actualizar la �ltima direcci�n
                lastDirection = direction;
            }
        }
    }

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }
}
