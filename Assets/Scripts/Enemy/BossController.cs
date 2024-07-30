using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public int maxHealth = 100;          // Vida máxima del boss
    private int currentHealth;           // Vida actual del boss

    public GameObject impactEffect;      // Prefab de la animación de impacto (opcional)
    public Text healthText;              // Referencia al componente Text para mostrar la vida
    public GameObject enemyPrefab;       // Prefab de los enemigos que se van a respawnear
    public Transform[] spawnPoints;      // Puntos de spawn para los enemigos

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText(); // Inicializar el texto de salud
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Proyectil"))
        {
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile != null)
            {
                TakeDamage(projectile.damage);
            }
        }

        if (other.CompareTag("Proyectil1"))
        {
            ProjectilElement projectileElement = other.GetComponent<ProjectilElement>();
            if (projectileElement != null)
            {
                TakeDamage(projectileElement.damage);
            }
        }
    }

    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthText();

        // Verificar los umbrales de vida y respawnear enemigos
        CheckHealthThresholds();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void CheckHealthThresholds()
    {
        if (currentHealth <= 99 && currentHealth > 80)
        {
            RespawnEnemies(10);
        }
        else if (currentHealth <= 80 && currentHealth > 60)
        {
            RespawnEnemies(15);
        }
        else if (currentHealth <= 60 && currentHealth > 40)
        {
            RespawnEnemies(20);
        }
        else if (currentHealth <= 40 && currentHealth > 20)
        {
            RespawnEnemies(25);
        }
        else if (currentHealth <= 20)
        {
            RespawnEnemies(30);
        }
    }

    private void RespawnEnemies(int count)
    {
        // Respawnear enemigos en puntos de spawn disponibles
        for (int i = 0; i < count; i++)
        {
            if (spawnPoints.Length == 0) return; // Asegúrate de tener puntos de spawn
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    private void Die()
    {
        // Desactivar el componente SpriteRenderer para que el boss sea invisible
        GetComponent<SpriteRenderer>().enabled = false;

        // Instanciar la animación de impacto si está configurada
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }

        // Destruir el boss después de 2 segundos
        StartCoroutine(HandleDeath());
    }

    private IEnumerator HandleDeath()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void UpdateHealthText()
    {
        // Mostrar la vida actual en el componente Text
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
    }
}
