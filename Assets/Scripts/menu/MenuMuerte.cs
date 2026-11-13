using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// =====================================================
// MenuMuerte.cs
// Muestra el resultado final y permite reiniciar o salir.
// Adjuntar a: el Panel de Game Over (PanelMuerte)
// =====================================================
public class MenuMuerte : MonoBehaviour
{
    // === UI ===
    [Header("Textos de resultado")]
    [Tooltip("Texto donde se muestran los puntos finales")]
    public TMP_Text textoPuntos;

    [Tooltip("Texto donde se muestra el tiempo sobrevivido")]
    public TMP_Text textoTiempo;

    // =====================================================
    // CICLO DE VIDA UNITY
    // =====================================================

    void OnEnable()
    {
        // Cuando el panel se activa, mostramos los resultados
        MostrarResultados();

        // Pausar el juego
        Time.timeScale = 0f;
    }

    // =====================================================
    // MÉTODOS PRIVADOS
    // =====================================================

    void MostrarResultados()
    {
        if (PuntajeYTiempo.Instancia == null) return;

        if (textoPuntos != null)
            textoPuntos.text = "Puntos: " + PuntajeYTiempo.Instancia.ObtenerPuntos();

        if (textoTiempo != null)
            textoTiempo.text = "Tiempo: " + PuntajeYTiempo.Instancia.ObtenerTiempoFormateado();
    }

    // =====================================================
    // BOTONES (asignar en el Inspector → Button → OnClick)
    // =====================================================

    // Botón "Reiniciar"
    public void Reiniciar()
    {
        Time.timeScale = 1f; // reanudar el tiempo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Botón "Salir"
    public void Salir()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
