using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 1.5f;
    public int damage = 1;
    public GameObject impactEffect; // Referencia al efecto de impacto

    private Vector2 moveDirection;

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;

        // Ajustar la rotaci�n del proyectil para que apunte en la direcci�n del movimiento
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Ajuste de �ngulo si el sprite est� orientado hacia la derecha por defecto
    }

    void Update()
    {
        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);

        // Destruye el proyectil despu�s de un tiempo
        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            DestroyProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el proyectil colision� con un objeto con la etiqueta "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Aplica el da�o al enemigo aqu� si es necesario

            // Destruye el proyectil
            DestroyProjectile();
        }
    }

    private void DestroyProjectile()
    {
        // Instancia el efecto de impacto si est� configurado
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        // Destruye el proyectil
        Destroy(gameObject);
    }
}





