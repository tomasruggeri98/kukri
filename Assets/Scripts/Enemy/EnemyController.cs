using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public int maxHealth = 100; // Vida m�xima del slime
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

    // M�todo para recibir da�o
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Proyectil"))
        {
            // Obtener el da�o del proyectil y causarlo al slime
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile != null)
            {
                TakeDamage(projectile.damage);
            }
        }

        if (other.CompareTag("Proyectil1"))
        {
            // Obtener el da�o del proyectil y causarlo al slime
            ProjectilElement projectileElement = other.GetComponent<ProjectilElement>();
            if (projectileElement != null)
            {
                TakeDamage(projectileElement.damage);
            }
        }
    }

    // M�todo para causar da�o al slime
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // M�todo para manejar la muerte del slime
    void Die()
    {
        // Detener el movimiento
        if (enemyChaser != null)
        {
            enemyChaser.enabled = false;
        }

        // Desactivar el SpriteRenderer para hacer invisible al slime
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }


        // Destruir el objeto despu�s de 0.6 segundos
        Destroy(gameObject, 0.6f);
    }
}
