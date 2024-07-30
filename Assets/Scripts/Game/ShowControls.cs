using System.Collections;
using UnityEngine;

public class ShowControls : MonoBehaviour
{
    public GameObject controlsPanel; // Referencia al panel de controles
    public float showDelay = 2f;      // Tiempo antes de mostrar el panel
    public float hideDelay = 3f;      // Tiempo que el panel estará visible antes de ocultarse

    void Start()
    {
        // Iniciar la corutina para manejar la visibilidad del panel
        StartCoroutine(ManageControlsPanel());
    }

    IEnumerator ManageControlsPanel()
    {
        // Esperar el tiempo de retraso antes de mostrar el panel
        yield return new WaitForSeconds(showDelay);

        // Mostrar el panel de controles
        controlsPanel.SetActive(true);

        // Esperar el tiempo que el panel estará visible
        yield return new WaitForSeconds(hideDelay);

        // Ocultar el panel de controles
        controlsPanel.SetActive(false);
    }
}
