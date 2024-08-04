using System.Collections;
using UnityEngine;

public class CubeEnemyController : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento del enemigo
    public int maxHealth = 100; // Vida m�xima del enemigo
    private int currentHealth; // Vida actual del enemigo

    private Transform player; // Referencia al jugador
    private Vector2 lastDirection; // �ltima direcci�n en la que se movi� el enemigo

    public float rotationSpeed = 100f; // Velocidad de rotaci�n del sprite
    public GameObject fireworkPrefab; // Prefab de la animaci�n de explosi�n
    public AudioSource deathAudioSource; // AudioSource para la muerte

    void Start()
    {
        currentHealth = maxHealth;
        // Buscar autom�ticamente al jugador al inicio
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player == null)
        {
            Debug.LogError("No se pudo encontrar al jugador. Aseg�rate de que tiene el tag 'Player'.");
        }

        deathAudioSource = GetComponent<AudioSource>(); // Aseg�rate de tener un AudioSource en el objeto
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
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else if (direction.x < 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }

                // Actualizar la �ltima direcci�n
                lastDirection = direction;
            }

            // Hacer que el sprite gire constantemente
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }
    }

    // M�todo para recibir da�o
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Proyectil"))
        {
            // Obtener el da�o del proyectil y causarlo al cubo
            Projectile projectile = other.GetComponent<Projectile>();
            if (projectile != null)
            {
                TakeDamage(projectile.damage);
            }
        }

        if (other.CompareTag("Proyectil1"))
        {
            // Obtener el da�o del proyectil y causarlo al cubo
            ProjectilElement projectileElement = other.GetComponent<ProjectilElement>();
            if (projectileElement != null)
            {
                TakeDamage(projectileElement.damage);
            }
        }
    }

    // M�todo para causar da�o al cubo
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // M�todo para manejar la muerte del cubo
    void Die()
    {
        // Detener el movimiento
        StopMovement();

        // Desactivar el componente SpriteRenderer para que el cubo sea invisible
        GetComponent<SpriteRenderer>().enabled = false;

        // Instanciar la animaci�n de explosi�n en la posici�n del cubo
        GameObject firework = Instantiate(fireworkPrefab, transform.position, transform.rotation);

        // Reproducir sonido de muerte
        if (deathAudioSource != null)
        {
            deathAudioSource.Play();
        }

        // Destruir el cubo despu�s de 2 segundos
        StartCoroutine(HandleDeath(firework));
    }

    // M�todo para detener el movimiento del cubo
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

        // Destruir la animaci�n de fuegos artificiales despu�s de 1 segundo
        Destroy(firework, 0.6f);
    }
}
