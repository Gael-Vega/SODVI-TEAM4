
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject panelGameOver;

    void Start()
    {
        // Ocultamos el panel al comenzar la partida.
        panelGameOver.SetActive(false);
    }

    public void MostrarGameOver()
    {
        // Mostramos la pantalla de Game Over.
        panelGameOver.SetActive(true);

        // Detenemos el juego.
        Time.timeScale = 0f;
    }
}
