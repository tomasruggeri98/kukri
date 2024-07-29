using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public float speed = 5f;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 0.2f; // Intervalo de tiempo entre disparos

    private Vector2 moveDirection;
    private Vector2 lastMoveDirection;
    private Animator playerAnimator;
    private float nextFireTime = 0f; // Tiempo del próximo disparo permitido

    // Limites del mapa
    private Vector2 minBounds = new Vector2(-67f, -35f);
    private Vector2 maxBounds = new Vector2(72f, 34f);

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimiento del jugador
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;

        if (moveDirection != Vector2.zero)
        {
            lastMoveDirection = moveDirection;
        }

        // Nueva posición calculada
        Vector3 newPosition = transform.position + (Vector3)(moveDirection * speed * Time.deltaTime);

        // Limitar la posición del jugador a los límites del mapa
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

        // Aplicar la nueva posición al transform
        transform.position = newPosition;

        // Actualizar la dirección del punto de disparo
        UpdateFirePointDirection();

        // Disparar proyectil
        if (Input.GetKey(KeyCode.Alpha1) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Actualizar el tiempo del próximo disparo permitido
        }

        // Actualizar animaciones
        playerAnimator.SetFloat("horizontal", moveX);
        playerAnimator.SetFloat("vertical", moveY);
        playerAnimator.SetFloat("speed", moveDirection.sqrMagnitude);
    }

    void UpdateFirePointDirection()
    {
        if (lastMoveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(lastMoveDirection.y, lastMoveDirection.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            // Usa la última dirección de movimiento si el jugador está quieto
            projectileScript.SetDirection(lastMoveDirection != Vector2.zero ? lastMoveDirection : Vector2.right); // Dirección predeterminada si no hay movimiento
        }
    }

    public void IncreaseSpeed(float amount)
    {
        speed += amount;
    }
}


