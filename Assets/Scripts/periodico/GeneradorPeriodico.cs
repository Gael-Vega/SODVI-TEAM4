using System.Collections;
using UnityEngine;

// =====================================================
// GeneradorPeriodico.cs
// Genera periódicos a intervalos regulares desde los bordes.
// Adjuntar a: un GameObject vacío en la escena (ej. "GeneradorPeriodico")
// =====================================================
public class GeneradorPeriodico : MonoBehaviour
{
    // === CONFIGURACIÓN ===
    [Header("Prefab")]
    [Tooltip("Prefab del periódico que ataca")]
    public GameObject prefabPeriodico;

    [Header("Tiempos")]
    [Tooltip("Segundos antes del primer periódico")]
    public float retrasoInicial = 2f;

    [Tooltip("Intervalo mínimo entre periódicos (segundos)")]
    public float intervaloMinimo = 4f;

    [Tooltip("Intervalo máximo entre periódicos (segundos)")]
    public float intervaloMaximo = 5f;

    [Header("Área de aparición")]
    [Tooltip("Límite horizontal de la pantalla")]
    public float xBorde = 5f;

    [Tooltip("Límite vertical de la pantalla")]
    public float yBorde = 4f;

    // =====================================================
    // CICLO DE VIDA UNITY
    // =====================================================

    void Start()
    {
        StartCoroutine(CorrutinaGenerarPeriodicos());
    }

    // =====================================================
    // CORRUTINA PRINCIPAL
    // =====================================================

    IEnumerator CorrutinaGenerarPeriodicos()
    {
        yield return new WaitForSeconds(retrasoInicial);

        while (true)
        {
            CrearPeriodico();

            float espera = Random.Range(intervaloMinimo, intervaloMaximo);
            yield return new WaitForSeconds(espera);
        }
    }

    // =====================================================
    // MÉTODOS DE CREACIÓN
    // =====================================================

    void CrearPeriodico()
    {
        if (prefabPeriodico == null) return;

        Vector3 posicion = ElegirBorde();
        Instantiate(prefabPeriodico, posicion, Quaternion.identity);
    }

    // Elige un punto aleatorio en uno de los 4 bordes de la pantalla
    Vector3 ElegirBorde()
    {
        int borde = Random.Range(0, 4); // 0=arriba, 1=abajo, 2=izquierda, 3=derecha

        switch (borde)
        {
            case 0: return new Vector3(Random.Range(-xBorde, xBorde),  yBorde, 0);
            case 1: return new Vector3(Random.Range(-xBorde, xBorde), -yBorde, 0);
            case 2: return new Vector3(-xBorde, Random.Range(-yBorde, yBorde), 0);
            default: return new Vector3( xBorde, Random.Range(-yBorde, yBorde), 0);
        }
    }
}
