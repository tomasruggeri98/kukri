using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public int maxHealth = 100; // Vida m�xima del slime
    private int currentHealth;   // Vida actual del slime
    



    void Start()
    {
        currentHealth = maxHealth;
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
            ProjectilElement ProjectileElement = other.GetComponent<ProjectilElement>();
            if (ProjectileElement != null)
            {
                TakeDamage(ProjectileElement.damage);
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
        
        
        
        

        Destroy(gameObject);
    }
}
