using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public OrbitingBalls orbitingBalls;  // Referencia al script de orbitación

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))  // Asegúrate de que el objeto activador tenga la etiqueta "Activator"
        {
            
            orbitingBalls.ActivateBalls();
        }
    }
}
