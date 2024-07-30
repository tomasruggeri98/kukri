using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // Referencia al texto que muestra el puntaje
    private int score = 0; // Puntaje actual
    public float scoreInterval = 1f; // Intervalo de tiempo entre incrementos de puntaje

    private float timer = 0f; // Temporizador para controlar el incremento de puntaje

    // Método para obtener el puntaje actual
    public int GetScore()
    {
        return score;
    }

    // Método para restablecer el puntaje
    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    void Update()
    {
        // Incrementar el temporizador
        timer += Time.deltaTime;

        // Verificar si ha pasado el intervalo de tiempo para aumentar el puntaje
        if (timer >= scoreInterval)
        {
            // Aumentar el puntaje
            score++;
            // Actualizar el texto del puntaje
            UpdateScoreText();
            // Reiniciar el temporizador
            timer = 0f;
        }
    }

    // Método para actualizar el texto del puntaje
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
