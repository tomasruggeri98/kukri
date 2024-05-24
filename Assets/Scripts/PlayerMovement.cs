using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    // Referencia al Rigidbody2D del jugador
    private Rigidbody2D rb2d;
    private Animator playerAnimator;

    public float speed = 5f;
    public GameObject projectilePrefab;
    public Transform firePoint;

    private Vector2 moveDirection;

    void Start()
    {
        // Obtener el componente Rigidbody2D del jugador
        rb2d = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimiento del jugador
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);

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
        // Instanciar el proyectil
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            projectileScript.SetDirection(moveDirection);
        }

        // Rotar el proyectil en la dirección del movimiento
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 180f));
    }
}
