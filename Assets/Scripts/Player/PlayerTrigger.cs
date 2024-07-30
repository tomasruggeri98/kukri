using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Necesario para la UI

public class PlayerTrigger : MonoBehaviour
{
    public OrbitingBalls orbitingBalls;  // Referencia al script de orbitaci�n
    public int maxHealth = 3; // Vida m�xima del jugador
    private int currentHealth; // Vida actual del jugador
    public Text healthText; // Referencia al texto de la UI para mostrar la vida
    private bool canTakeDamage = true; // Permite controlar cu�ndo el jugador puede recibir da�o

    void Start()
    {
        currentHealth = maxHealth; // Inicializa la vida actual
        UpdateHealthUI(); // Muestra la vida inicial en la UI
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))  // Aseg�rate de que el objeto activador tenga la etiqueta "Activator"
        {
            orbitingBalls.ActivateNextBall();
        }

        if (other.CompareTag("Enemy") && canTakeDamage)
        {
            StartCoroutine(TakeDamageCoroutine(1)); // Reduce la vida del jugador al recibir da�o
        }
    }

    IEnumerator TakeDamageCoroutine(int damage)
    {
        if (canTakeDamage)
        {
            canTakeDamage = false; // Desactiva temporalmente la posibilidad de recibir da�o

            TakeDamage(damage); // Reduce la vida actual

            yield return new WaitForSeconds(2f); // Espera 2 segundos

            canTakeDamage = true; // Reactiva la posibilidad de recibir da�o
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce la vida actual
        if (currentHealth <= 0)
        {

            currentHealth = 0;
            PlayerPrefs.SetInt("LastScore", FindObjectOfType<ScoreManager>().GetScore());
            SceneManager.LoadScene("Menu"); // Cargar la escena del men� si se ha quedado sin vida
        }
        UpdateHealthUI(); // Actualiza la UI con la vida restante
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "" + currentHealth; // Actualiza el texto de la UI con la vida actual
        }
    }
}
