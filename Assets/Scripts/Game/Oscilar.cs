using UnityEngine;

public class Oscilar : MonoBehaviour
{
    public float amplitude = 1f; // Amplitud del movimiento
    public float frequency = 1f; // Frecuencia del movimiento

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = startPosition + new Vector3(0, offset, 0); // Movimiento vertical
        // Para movimiento horizontal, usa: new Vector3(offset, 0, 0);
    }
}
