using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// =====================================================
// SaludMosca.cs
// Controla las vidas de la mosca y el game over.
// Adjuntar a: el GameObject de la Mosca
// =====================================================
public class SaludMosca : MonoBehaviour
{
    // === CONFIGURACIÓN ===
    [Header("Vidas")]
    [Tooltip("Vidas máximas que tiene la mosca")]
    public int vidasMaximas = 3;

    [Tooltip("Segundos de invulnerabilidad tras recibir daño")]
    public float tiempoInvulnerable = 1.5f;

    // === UI ===
    [Header("Interfaz")]
    [Tooltip("Imágenes de corazón (una por vida)")]
    public Image[] iconosCorazones;

    [Tooltip("Sprite de corazón lleno")]
    public Sprite corazonLleno;

    [Tooltip("Sprite de corazón vacío")]
    public Sprite corazonVacio;

    [Tooltip("Panel que aparece al morir (Game Over)")]
    public GameObject panelGameOver;

    // === ESTADO (privado) ===
    private int _vidasActuales;
    private bool _esInvulnerable = false;
    private bool _estaMuerto = false;

    // Propiedad pública de solo lectura
    public bool EstaMuerto => _estaMuerto;

    // =====================================================
    // CICLO DE VIDA UNITY
    // =====================================================

    void Start()
    {
        _vidasActuales = vidasMaximas;
        ActualizarCorazones();

        if (panelGameOver != null)
            panelGameOver.SetActive(false);
    }

    // =====================================================
    // MÉTODOS PÚBLICOS (llamados por otros scripts)
    // =====================================================

    // Recibe 1 unidad de daño (respeta invulnerabilidad)
    public void RecibirDanio(int cantidad)
    {
        if (_esInvulnerable || _estaMuerto) return;

        _vidasActuales -= cantidad;
        ActualizarCorazones();

        if (_vidasActuales <= 0)
            Morir();
        else
            StartCoroutine(CorrutinaInvulnerabilidad());
    }

    // Mata a la mosca sin importar invulnerabilidad (raqueta)
    public void Instakill()
    {
        if (_estaMuerto) return;

        _vidasActuales = 0;
        ActualizarCorazones();
        Morir();
    }

    // =====================================================
    // MÉTODOS PRIVADOS
    // =====================================================

    void Morir()
    {
        _estaMuerto = true;

        // Detener el contador de puntos
        if (PuntajeYTiempo.Instancia != null)
            PuntajeYTiempo.Instancia.DetenerJuego();

        // Mostrar pantalla de muerte
        if (panelGameOver != null)
            panelGameOver.SetActive(true);

        // Desactivar al jugador
        gameObject.SetActive(false);
    }

    void ActualizarCorazones()
    {
        // Recorre cada ícono y lo llena o vacía según las vidas
        for (int i = 0; i < iconosCorazones.Length; i++)
        {
            if (iconosCorazones[i] == null) continue;

            bool tieneVida = i < _vidasActuales;
            iconosCorazones[i].sprite = tieneVida ? corazonLleno : corazonVacio;
        }
    }

    // Corrutina: pausa el daño durante unos segundos
    IEnumerator CorrutinaInvulnerabilidad()
    {
        _esInvulnerable = true;
        yield return new WaitForSeconds(tiempoInvulnerable);
        _esInvulnerable = false;
    }
}
