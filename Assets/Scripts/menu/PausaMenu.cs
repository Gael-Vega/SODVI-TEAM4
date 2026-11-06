using UnityEngine;
using UnityEngine.SceneManagement;

public class PausaMenu : MonoBehaviour
{
    public GameObject pausaPanel;
    public GameObject opcionesBoton;

    public void PausaJuego()
    {
        Time.timeScale = 0;
        opcionesBoton.SetActive(false);
        pausaPanel.SetActive(true);

    }


    public void ReanudarJuego()
    {
        Time.timeScale = 1;
        opcionesBoton.SetActive(true);
        pausaPanel.SetActive(false);

    }


    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;

    }


    public void SalirJuego()
    {
        Debug.Log("Cerrar juego");
    Application.Quit();


    }


}


   


