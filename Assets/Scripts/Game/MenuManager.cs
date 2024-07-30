using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour


{

    void Start()
    {
        // Obtener el puntaje guardado y mostrarlo
        int lastScore = PlayerPrefs.GetInt("LastScore", 0);
        scoreText.text = "Score de la ultima sesion: " + lastScore.ToString();
    }

    public Text scoreText;
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
