using UnityEngine;
using System.Collections;

public class SpeedPowerUp : MonoBehaviour
{
    public float speedIncrease = 1f; // Aumento de la velocidad
    public float destroyDelay = 1f; // Tiempo antes de destruir el power-up

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement2D playerMovement = other.GetComponent<PlayerMovement2D>();
            if (playerMovement != null)
            {
                playerMovement.IncreaseSpeed(speedIncrease);
                StartCoroutine(DestroyAfterDelay());
            }
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
