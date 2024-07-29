using UnityEngine;
using System.Collections.Generic;

public class OrbitingBalls : MonoBehaviour
{
    public Transform player;  // Referencia al jugador
    public float orbitRadius = 2f;  // Radio de la órbita
    public float orbitSpeed = 50f;  // Velocidad de la órbita
    public List<GameObject> balls = new List<GameObject>();  // Lista de las bolas que orbitan

    private bool ballsActivated = false; // Para evitar activarlas múltiples veces
    private int nextBallIndex = 0; // Índice de la siguiente bola a activar
    private bool expanding = true; // Controla si las bolas están expandiéndose o contrayéndose
    public float expandRadius = 4f;  // Radio de expansión máximo
    public float expandSpeed = 2f;  // Velocidad de expansión y contracción

    void Update()
    {
        if (!ballsActivated) return;

        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i].activeSelf)
            {
                // Calcular el ángulo de la órbita
                float angle = (Time.time * orbitSpeed + (i * 360f / balls.Count)) * Mathf.Deg2Rad;

                // Calcular la distancia actual basada en si se están expandiendo o contrayendo
                float currentRadius = expanding ? Mathf.Lerp(orbitRadius, expandRadius, Mathf.PingPong(Time.time * expandSpeed, 1))
                                                : Mathf.Lerp(expandRadius, orbitRadius, Mathf.PingPong(Time.time * expandSpeed, 1));

                // Calcular la posición de la bola
                Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * currentRadius;
                balls[i].transform.position = (Vector2)player.position + offset;
            }
        }
    }

    public void ActivateNextBall()
    {
        if (nextBallIndex < balls.Count)
        {
            balls[nextBallIndex].SetActive(true);
            nextBallIndex++;
            if (nextBallIndex == balls.Count)
            {
                ballsActivated = true;
            }
        }
    }

    // Método para añadir una nueva bola a la lista y desactivarla inicialmente
    public void AddBall(GameObject newBall)
    {
        newBall.SetActive(false);
        balls.Add(newBall);
    }

    // Método para resetear el índice y desactivar las bolas
    public void ResetBalls()
    {
        foreach (var ball in balls)
        {
            ball.SetActive(false);
        }
        nextBallIndex = 0;
        ballsActivated = false;
    }
}

