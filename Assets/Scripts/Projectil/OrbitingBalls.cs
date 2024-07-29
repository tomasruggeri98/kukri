using UnityEngine;
using System.Collections.Generic;

public class OrbitingBalls : MonoBehaviour
{
    public Transform player;  // Referencia al jugador
    public float orbitRadius = 2f;  // Radio de la �rbita
    public float orbitSpeed = 50f;  // Velocidad de la �rbita
    public List<GameObject> balls = new List<GameObject>();  // Lista de las bolas que orbitan

    private bool ballsActivated = false; // Para evitar activarlas m�ltiples veces
    private int nextBallIndex = 0; // �ndice de la siguiente bola a activar
    private bool expanding = true; // Controla si las bolas est�n expandi�ndose o contray�ndose
    public float expandRadius = 4f;  // Radio de expansi�n m�ximo
    public float expandSpeed = 2f;  // Velocidad de expansi�n y contracci�n

    void Update()
    {
        if (!ballsActivated) return;

        for (int i = 0; i < balls.Count; i++)
        {
            if (balls[i].activeSelf)
            {
                // Calcular el �ngulo de la �rbita
                float angle = (Time.time * orbitSpeed + (i * 360f / balls.Count)) * Mathf.Deg2Rad;

                // Calcular la distancia actual basada en si se est�n expandiendo o contrayendo
                float currentRadius = expanding ? Mathf.Lerp(orbitRadius, expandRadius, Mathf.PingPong(Time.time * expandSpeed, 1))
                                                : Mathf.Lerp(expandRadius, orbitRadius, Mathf.PingPong(Time.time * expandSpeed, 1));

                // Calcular la posici�n de la bola
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

    // M�todo para a�adir una nueva bola a la lista y desactivarla inicialmente
    public void AddBall(GameObject newBall)
    {
        newBall.SetActive(false);
        balls.Add(newBall);
    }

    // M�todo para resetear el �ndice y desactivar las bolas
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

