using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void LoadGameScene()
    {
        // Cargar la escena del juego (SampleScene)
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        // Salir del juego
        Application.Quit();
    }
}
