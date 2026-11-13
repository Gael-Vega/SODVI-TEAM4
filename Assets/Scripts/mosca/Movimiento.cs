using System.Collections;
using UnityEngine;

/// <summary>
/// Controla el movimiento, físicas 2D y animaciones de la mosca.
/// Diseñado de forma modular siguiendo principios de Programación Orientada a Objetos (POO).
/// </summary>
public class Movimiento : MonoBehaviour
{
    // ========================================================
    // ATRIBUTOS Y ENCAPSULAMIENTO
    // ========================================================

    [Header("Configuración")]
    [Tooltip("Velocidad de traslación de la mosca.")]
    public float velocidad = 5f;

    [Tooltip("Bandera que determina si la mosca puede moverse o si está inmovilizada.")]
    public bool puedeMoverse = true;

    [Header("Animación")]
    [Tooltip("Nombre del parámetro en el Animator para subir.")]
    public string parametroSubir = "subir";

    // Componentes del objeto (encapsulados como privados)
    private Rigidbody2D rb2D;
    private Animator animator;

    // Estado interno
    private bool controlesInvertidos = false;

    // ========================================================
    // CICLO DE VIDA (MonoBehaviour)
    // ========================================================

    void Start()
    {
        // Inicialización y enlace de dependencias en tiempo de ejecución
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Cláusula de guarda: si no puede moverse, frenamos en seco y no procesamos entrada
        if (!puedeMoverse)
        {
            Frenar();
            return;
        }

        ProcesarMovimiento();
        ProcesarAnimaciones();
    }

    // ========================================================
    // MÉTODOS DE COMPORTAMIENTO (Modularidad / Cohesión)
    // ========================================================

    /// <summary>
    /// Lee la entrada del jugador, aplica inversión si está mareada y traslada el cuerpo físico.
    /// </summary>
    private void ProcesarMovimiento()
    {
        // 1. Lectura de entrada (-1, 0, 1)
        float entradaX = Input.GetAxisRaw("Horizontal");
        float entradaY = Input.GetAxisRaw("Vertical");

        // 2. Si el periódico la golpeó, invertimos la dirección multiplicando por -1
        float factor = controlesInvertidos ? -1f : 1f;
        float dirX = entradaX * factor;
        float dirY = entradaY * factor;

        // 3. Aplicar velocidad al componente de física
        rb2D.linearVelocity = new Vector2(dirX * velocidad, dirY * velocidad);

        // 4. Orientación del sprite (voltear en X según la dirección horizontal)
        if (dirX != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(dirX), 1, 1);
        }
    }

    /// <summary>
    /// Notifica al Animator cuando el jugador se desplaza hacia arriba.
    /// </summary>
    private void ProcesarAnimaciones()
    {
        if (animator == null) return;

        // Si se mueve hacia arriba (positivo en Y), activamos el trigger de subir
        float entradaY = Input.GetAxisRaw("Vertical");
        if (entradaY > 0.1f)
        {
            animator.SetTrigger(parametroSubir);
        }
    }

    /// <summary>
    /// Método público para iniciar el efecto de controles invertidos (usado por el Periódico).
    /// </summary>
    public void InvertirControles(float duracion = 5f)
    {
        StopCoroutine("RutinaInvertirControles");
        StartCoroutine(RutinaInvertirControles(duracion));
    }

    /// <summary>
    /// Corrutina asíncrona que gestiona la duración del mareo/inversión temporal.
    /// </summary>
    private IEnumerator RutinaInvertirControles(float duracion)
    {
        controlesInvertidos = true;
        if (animator != null) animator.SetBool("Mareada", true);

        Debug.Log($"<color=magenta><b>[Movimiento]</b> Controles invertidos por {duracion} segundos.</color>");

        // Pausa no bloqueante en tiempo de juego
        yield return new WaitForSeconds(duracion);

        controlesInvertidos = false;
        if (animator != null) animator.SetBool("Mareada", false);

        Debug.Log("<color=cyan><b>[Movimiento]</b> Controles restaurados a la normalidad.</color>");
    }

    // ========================================================
    // INTERFAZ PÚBLICA DE CONTROL
    // ========================================================

    public void BloquearMovimiento()
    {
        puedeMoverse = false;
        Frenar();
    }

    public void PermitirMovimiento()
    {
        puedeMoverse = true;
    }

    private void Frenar()
    {
        if (rb2D != null)
        {
            rb2D.linearVelocity = Vector2.zero;
        }
    }
}
