using UnityEngine;

public class OrbitingBalls : MonoBehaviour
{
    public Transform player;  // Referencia al jugador
    public float orbitRadius = 2f;  // Radio de la órbita
    public float orbitSpeed = 50f;  // Velocidad de la órbita
    public GameObject[] balls;  // Las bolas que orbitan

    private bool ballsActivated = false; // Para evitar activarlas múltiples veces

    void Update()
    {
        if (!ballsActivated) return;

        for (int i = 0; i < balls.Length; i++)
        {
            float angle = (Time.time * orbitSpeed + (i * 360f / balls.Length)) * Mathf.Deg2Rad;
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * orbitRadius;
            balls[i].transform.position = (Vector2)player.position + offset;
        }
    }

    public void ActivateBalls()
    {
        foreach (var ball in balls)
        {
            ball.SetActive(true);
        }
        ballsActivated = true;
    }
}
