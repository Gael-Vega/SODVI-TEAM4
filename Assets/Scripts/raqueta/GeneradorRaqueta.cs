using System.Collections;
using UnityEngine;

// =====================================================
// GeneradorRaqueta.cs
// Genera raquetas en posiciones aleatorias cada cierto tiempo.
// Adjuntar a: un GameObject vacío en la escena (ej. "GeneradorRaqueta")
// =====================================================
public class GeneradorRaqueta : MonoBehaviour
{
    // === CONFIGURACIÓN ===
    [Header("Prefabs")]
    [Tooltip("Prefab de la raqueta que cae")]
    public GameObject prefabRaqueta;

    [Tooltip("Prefab del triángulo de advertencia")]
    public GameObject prefabAdvertencia;

    [Header("Tiempos")]
    [Tooltip("Segundos antes de que aparezca la primera raqueta")]
    public float retrasoInicial = 15f;

    [Tooltip("Intervalo mínimo entre raquetas (segundos)")]
    public float intervaloMinimo = 10f;

    [Tooltip("Intervalo máximo entre raquetas (segundos)")]
    public float intervaloMaximo = 20f;

    [Tooltip("Segundos que se muestra la advertencia antes de la raqueta")]
    public float tiempoAdvertencia = 1.5f;

    [Header("Área de aparición")]
    [Tooltip("Límite izquierdo de la pantalla")]
    public float xMinima = -4f;

    [Tooltip("Límite derecho de la pantalla")]
    public float xMaxima = 4f;

    [Tooltip("Altura desde donde baja la raqueta")]
    public float alturaInicial = 6f;

    // =====================================================
    // CICLO DE VIDA UNITY
    // =====================================================

    void Start()
    {
        StartCoroutine(CorrutinaGenerarRaquetas());
    }

    // =====================================================
    // CORRUTINA PRINCIPAL
    // =====================================================

    IEnumerator CorrutinaGenerarRaquetas()
    {
        // Espera al inicio para dar tiempo al jugador
        yield return new WaitForSeconds(retrasoInicial);

        while (true)
        {
            // 1. Elegir posición X aleatoria
            float xAleatoria = Random.Range(xMinima, xMaxima);

            // 2. Mostrar advertencia
            MostrarAdvertencia(xAleatoria);

            // 3. Esperar mientras la advertencia parpadea
            yield return new WaitForSeconds(tiempoAdvertencia);

            // 4. Crear la raqueta en esa posición
            CrearRaqueta(xAleatoria);

            // 5. Esperar antes de la siguiente raqueta
            float espera = Random.Range(intervaloMinimo, intervaloMaximo);
            yield return new WaitForSeconds(espera);
        }
    }

    // =====================================================
    // MÉTODOS DE CREACIÓN
    // =====================================================

    void MostrarAdvertencia(float xPosicion)
    {
        if (prefabAdvertencia == null) return;

        Vector3 posicion = new Vector3(xPosicion, alturaInicial - 1f, 0f);
        GameObject advertencia = Instantiate(prefabAdvertencia, posicion, Quaternion.identity);

        // La advertencia se destruye sola después del tiempo
        Destroy(advertencia, tiempoAdvertencia);
    }

    void CrearRaqueta(float xPosicion)
    {
        if (prefabRaqueta == null) return;

        Vector3 posicion = new Vector3(xPosicion, alturaInicial, 0f);
        Instantiate(prefabRaqueta, posicion, Quaternion.identity);
    }
}
