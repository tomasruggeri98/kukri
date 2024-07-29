using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public float speed = 5f;
    public GameObject projectilePrefab;
    public Transform firePoint;

    private Vector2 moveDirection;
    private Vector2 lastMoveDirection;
    private Animator playerAnimator;

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

        // Disparar proyectil
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Shoot();
        }

        // Actualizar animaciones
        playerAnimator.SetFloat("horizontal", moveX);
        playerAnimator.SetFloat("vertical", moveY);
        playerAnimator.SetFloat("speed", moveDirection.sqrMagnitude);
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            // Usa la última dirección de movimiento si el jugador está quieto
            projectileScript.SetDirection(lastMoveDirection != Vector2.zero ? lastMoveDirection : Vector2.right); // Default direction if no movement
        }
    }
}

