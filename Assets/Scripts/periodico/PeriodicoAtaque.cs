using System.Collections;
using UnityEngine;

// =====================================================
// PeriodicoAtaque.cs
// El periódico aparece en la escena, persigue a la mosca
// y al tocarla le quita una vida e invierte sus controles.
// Adjuntar a: el Prefab del Periódico
// =====================================================
public class PeriodicoAtaque : MonoBehaviour
{
    // === CONFIGURACIÓN ===
    [Header("Movimiento")]
    [Tooltip("Velocidad a la que el periódico se mueve hacia la mosca")]
    public float velocidad = 3f;

    [Header("Efecto al golpear")]
    [Tooltip("Segundos que los controles quedan invertidos")]
    public float tiempoControlsInvertidos = 5f;

    [Tooltip("Daño que hace al tocar a la mosca")]
    public int danio = 1;

    // === REFERENCIAS PRIVADAS ===
    private Transform _mosca;       // objetivo a perseguir
    private bool _yaGolpeo = false; // para no golpear dos veces

    // =====================================================
    // CICLO DE VIDA UNITY
    // =====================================================

    void Start()
    {
        // Busca a la mosca por su tag
        GameObject objetoMosca = GameObject.FindWithTag("Player");
        if (objetoMosca != null)
            _mosca = objetoMosca.transform;

        // El periódico se autodestruye después de un tiempo
        Destroy(gameObject, 8f);
    }

    void Update()
    {
        PerseguirMosca();
    }

    // =====================================================
    // MÉTODOS PRIVADOS
    // =====================================================

    void PerseguirMosca()
    {
        if (_mosca == null) return;

        // Mover hacia la mosca
        transform.position = Vector2.MoveTowards(
            transform.position,
            _mosca.position,
            velocidad * Time.deltaTime
        );
    }

    // =====================================================
    // DETECCIÓN DE COLISIÓN
    // =====================================================

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (_yaGolpeo) return; // solo golpea una vez

        SaludMosca salud = otro.GetComponent<SaludMosca>();
        if (salud == null) return;

        _yaGolpeo = true;

        // Quitar vida
        salud.RecibirDanio(danio);

        // Invertir controles de la mosca
        Movimiento movimiento = otro.GetComponent<Movimiento>();
        if (movimiento != null)
            movimiento.InvertirControles(tiempoControlsInvertidos);

        Destroy(gameObject);
    }
}
