using System.Collections;
using UnityEngine;

public class CubeEnemyController : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento del enemigo
    public int maxHealth = 100; // Vida máxima del enemigo
    private int currentHealth; // Vida actual del enemigo

    private Transform player; // Referencia al jugador
    private Vector2 lastDirection; // Última dirección en la que se movió el enemigo

    public float rotationSpeed = 100f; // Velocidad de rotación del sprite
    public GameObject fireworkPrefab; // Prefab de la animación de explosión
    public AudioSource deathAudioSource; // AudioSource para la muerte

    void Start()
    {
        currentHealth = maxHealth;
        // Buscar automáticamente al jugador al inicio
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("No se pudo encontrar al jugador. Asegúrate de que tiene el tag 'Player'.");
        }

        deathAudioSource = GetComponent<AudioSource>(); // Asegúrate de tener un AudioSource en el objeto
    }

    void Update()
    {
        if (player != null)
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
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else if (direction.x < 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }

                // Actualizar la última dirección
                lastDirection = direction;
            }

            // Hacer que el sprite gire constantemente
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }

    // Método para recibir daño
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Proyectil"))
        {
            // Obtener el daño del proyectil y causarlo al cubo
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile != null)
            {
                TakeDamage(projectile.damage);
            }
        }

        if (other.CompareTag("Proyectil1"))
        {
            // Obtener el daño del proyectil y causarlo al cubo
            ProjectilElement projectileElement = other.GetComponent<ProjectilElement>();
            if (projectileElement != null)
            {
                TakeDamage(projectileElement.damage);
            }
        }
    }

    // Método para causar daño al cubo
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Método para manejar la muerte del cubo
    void Die()
    {
        // Detener el movimiento
        StopMovement();

        // Desactivar el componente SpriteRenderer para que el cubo sea invisible
        GetComponent<SpriteRenderer>().enabled = false;

        // Instanciar la animación de explosión en la posición del cubo
        GameObject firework = Instantiate(fireworkPrefab, transform.position, transform.rotation);

        // Reproducir sonido de muerte
        if (deathAudioSource != null)
        {
            deathAudioSource.Play();
        }

        // Destruir el cubo después de 2 segundos
        StartCoroutine(HandleDeath(firework));
    }

    // Método para detener el movimiento del cubo
    void StopMovement()
    {
        speed = 0; // Detener el movimiento
    }

    // Coroutine para manejar la muerte con retraso
    IEnumerator HandleDeath(GameObject firework)
    {
        // Esperar 2 segundos
        yield return new WaitForSeconds(2f);

        // Destruir el cubo
        Destroy(gameObject);

        // Destruir la animación de fuegos artificiales después de 1 segundo
        Destroy(firework, 0.6f);
    }
}
