using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public int maxHealth = 100; // Vida máxima del slime
    private int currentHealth;   // Vida actual del slime

    private EnemyChaser enemyChaser; // Referencia al script EnemyChaser
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer

    void Start()
    {
        currentHealth = maxHealth;

        // Obtener referencias a componentes
        enemyChaser = GetComponent<EnemyChaser>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Método para recibir daño
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Proyectil"))
        {
            // Obtener el daño del proyectil y causarlo al slime
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile != null)
            {
                TakeDamage(projectile.damage);
            }
        }

        if (other.CompareTag("Proyectil1"))
        {
            // Obtener el daño del proyectil y causarlo al slime
            ProjectilElement ProjectileElement = other.GetComponent<ProjectilElement>();
            if (ProjectileElement != null)
            {
                TakeDamage(ProjectileElement.damage);
            }
        }
    }

    // Método para causar daño al slime
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método para manejar la muerte del slime
    void Die()
    {
        // Detener el movimiento del enemigo
        if (enemyChaser != null)
        {
            enemyChaser.StopMovement();
        }

        // Hacer el slime invisible
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }


        // Iniciar la coroutine para manejar la muerte con retraso
        StartCoroutine(HandleDeath());
    }

    // Coroutine para manejar la muerte con retraso
    IEnumerator HandleDeath()
    {
        // Esperar 2 segundos
        yield return new WaitForSeconds(2f);

        // Destruir el slime
        Destroy(gameObject);
    }
}

