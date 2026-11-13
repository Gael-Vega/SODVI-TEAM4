using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// =====================================================
// PuntajeYTiempo.cs
// Lleva el contador de tiempo, los puntos y la barra de progreso.
// Adjuntar a: un GameObject vacío en la escena (ej. "GestorPuntaje")
// =====================================================
public class PuntajeYTiempo : MonoBehaviour
{
    // === SINGLETON ===
    // Permite acceder desde cualquier script con: PuntajeYTiempo.Instancia
    public static PuntajeYTiempo Instancia;

    // === CONFIGURACIÓN ===
    [Header("Puntos")]
    [Tooltip("Puntos que se ganan por segundo de supervivencia")]
    public int puntosPorSegundo = 10;

    [Tooltip("Puntos máximos para llenar la barra")]
    public int puntosMaximos = 1000;

    // === UI ===
    [Header("Interfaz")]
    [Tooltip("Texto que muestra el tiempo")]
    public TMP_Text textoTiempo;

    [Tooltip("Texto que muestra los puntos")]
    public TMP_Text textoPuntos;

    [Tooltip("Imagen de la barra de progreso (fillAmount)")]
    public Image barraProgresoRelleno;

    // === ESTADO (privado) ===
    private float _tiempoTranscurrido = 0f;
    private int _puntosActuales = 0;
    private bool _juegoActivo = true;

    // =====================================================
    // CICLO DE VIDA UNITY
    // =====================================================

    void Awake()
    {
        // Configurar el Singleton
        Instancia = this;
    }

    void Update()
    {
        if (!_juegoActivo) return;

        ContarTiempo();
        ActualizarUI();
    }

    // =====================================================
    // MÉTODOS PRIVADOS
    // =====================================================

    void ContarTiempo()
    {
        _tiempoTranscurrido += Time.deltaTime;

        // Sumar puntos cada segundo completo
        int segundosEnteros = Mathf.FloorToInt(_tiempoTranscurrido);
        _puntosActuales = segundosEnteros * puntosPorSegundo;
    }

    void ActualizarUI()
    {
        // Formatear tiempo como MM:SS
        int minutos = Mathf.FloorToInt(_tiempoTranscurrido / 60f);
        int segundos = Mathf.FloorToInt(_tiempoTranscurrido % 60f);

        if (textoTiempo != null)
            textoTiempo.text = $"{minutos:00}:{segundos:00}";

        if (textoPuntos != null)
            textoPuntos.text = _puntosActuales.ToString();

        // Barra de progreso: va de 0 a 1 según los puntos máximos
        if (barraProgresoRelleno != null)
            barraProgresoRelleno.fillAmount = (float)_puntosActuales / puntosMaximos;
    }

    // =====================================================
    // MÉTODOS PÚBLICOS
    // =====================================================

    // Detiene el juego (llamado por SaludMosca al morir)
    public void DetenerJuego()
    {
        _juegoActivo = false;
    }

    // Devuelve los puntos actuales (para mostrar en el menú de muerte)
    public int ObtenerPuntos()
    {
        return _puntosActuales;
    }

    // Devuelve el tiempo en formato de texto
    public string ObtenerTiempoFormateado()
    {
        int minutos = Mathf.FloorToInt(_tiempoTranscurrido / 60f);
        int segundos = Mathf.FloorToInt(_tiempoTranscurrido % 60f);
        return $"{minutos:00}:{segundos:00}";
    }
}
