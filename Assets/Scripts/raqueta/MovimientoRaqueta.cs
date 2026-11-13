using System.Collections;
using UnityEngine;

// =====================================================
// MovimientoRaqueta.cs
// La raqueta baja desde arriba y aplasta a la mosca.
// Adjuntar a: el Prefab de la Raqueta
// =====================================================
public class MovimientoRaqueta : MonoBehaviour
{
    // === CONFIGURACIÓN ===
    [Header("Movimiento")]
    [Tooltip("Velocidad a la que baja la raqueta")]
    public float velocidadBajada = 8f;

    [Tooltip("Posición Y donde la raqueta golpea (el suelo)")]
    public float posicionYFinal = -5f;

    [Tooltip("Segundos que espera antes de desaparecer tras el golpe")]
    public float tiempoEspera = 0.5f;

    // === COMPONENTES ===
    private Collider2D _colisionador;

    // =====================================================
    // CICLO DE VIDA UNITY
    // =====================================================

    void Start()
    {
        _colisionador = GetComponent<Collider2D>();

        // El colisionador empieza apagado; se activa al llegar abajo
        if (_colisionador != null)
            _colisionador.enabled = false;

        StartCoroutine(CorrutinaBajar());
    }

    // =====================================================
    // CORRUTINA PRINCIPAL
    // =====================================================

    // La raqueta baja, golpea y desaparece
    IEnumerator CorrutinaBajar()
    {
        // --- BAJAR ---
        while (transform.position.y > posicionYFinal)
        {
            float nuevaY = transform.position.y - velocidadBajada * Time.deltaTime;
            transform.position = new Vector2(transform.position.x, nuevaY);
            yield return null; // espera un frame
        }

        // --- IMPACTO ---
        if (_colisionador != null)
            _colisionador.enabled = true; // activa el golpe

        yield return new WaitForSeconds(tiempoEspera);

        // --- DESTRUIR ---
        Destroy(gameObject);
    }

    // =====================================================
    // DETECCIÓN DE COLISIÓN
    // =====================================================

    void OnTriggerEnter2D(Collider2D otro)
    {
        // ¿Chocó con la mosca?
        SaludMosca salud = otro.GetComponent<SaludMosca>();
        if (salud != null)
            salud.Instakill(); // mata instantáneamente
    }
}
