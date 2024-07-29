using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float lifetime = 0.3f; // Tiempo de vida antes de destruir el objeto

    void Start()
    {
        // Destruir el objeto despu�s del tiempo especificado
        Destroy(gameObject, lifetime);
    }
}
