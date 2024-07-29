using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaser : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento del enemigo

    private Transform player; // Referencia al jugador
    private Vector2 lastDirection; // Última dirección en la que se movió el enemigo
    private bool isAlive = true; // Indica si el enemigo está vivo
    private Animator animator; // Referencia al Animator

    void Start()
    {
        // Buscar automáticamente al jugador al inicio
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("No se pudo encontrar al jugador. Asegúrate de que tiene el tag 'Player'.");
        }

        // Obtener referencia al Animator (si está presente)
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isAlive && player != null)
        {
            // Calcular la dirección hacia el jugador
            Vector2 direction = (player.position - transform.position).normalized;

            // Mover al enemigo hacia el jugador
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            // Si la dirección ha cambiado, girar el sprite del enemigo
            if (direction != lastDirection)
            {
                // Girar el sprite según la dirección en la que se está moviendo
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

                // Actualizar la última dirección
                lastDirection = direction;
            }
        }
    }

    // Método para detener el movimiento del enemigo
    public void StopMovement()
    {
        isAlive = false; // Indica que el enemigo está muerto y detener el movimiento

        // Desactivar el Animator si está presente
        if (animator != null)
        {
            animator.enabled = false;
        }
    }

    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }
}
