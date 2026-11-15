//using UnityEngine;

//public class EfectoMosca : MonoBehaviour
//{
// Start is called once before the first execution of Update after the MonoBehaviour is created
//void Start()
//  {

//}

// Update is called once per frame
//void Update()
//{

//}
//}



using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EfectosMosca : MonoBehaviour
{
    [Header("Referencias")]
    public Animator animator;

    [Header("Duracion de los efectos")]
    public float duracionImpactoPeriodico = 0.5f;
    public float duracionInvertido = 10f;
    public float duracionVeneno = 7f;
    public float duracionElectrocucion = 1.5f;

    [Header("Velocidad durante el veneno")]
    public float multiplicadorVeneno = 0.65f;

    [Header("Accion al terminar la electrocucion")]
    public UnityEvent alTerminarElectrocucion;

    // Indica si los controles estan invertidos.
    public bool ControlesInvertidos { get; private set; }

    // Indica cuanto se modifica la velocidad.
    public float MultiplicadorVelocidad { get; private set; } = 1f;

    // Indica si la partida termino.
    public bool PartidaTerminada { get; private set; }

    // Evita recibir dos efectos simultaneamente.
    private bool recibiendoEfecto = false;


    // ==========================================
    // ATAQUE 1: PERIODICO
    // ==========================================

    public void RecibirPeriodico()
    {
        if (recibiendoEfecto || PartidaTerminada)
            return;

        StartCoroutine(EfectoPeriodico());
    }

    private IEnumerator EfectoPeriodico()
    {
        recibiendoEfecto = true;

        // Activa la animacion de impacto.
        animator.SetTrigger("impactoPeriodico");

        // Espera a que termine el impacto.
        yield return new WaitForSeconds(duracionImpactoPeriodico);

        // Invierte los controles.
        ControlesInvertidos = true;

        // Espera los 10 segundos.
        yield return new WaitForSeconds(duracionInvertido);

        // Recupera los controles normales.
        ControlesInvertidos = false;

        // Regresa a la animacion normal.
        animator.SetTrigger("finInvertido");

        recibiendoEfecto = false;
    }


    // ==========================================
    // ATAQUE 2: RAQUETA ELECTRICA
    // ==========================================

    public void RecibirRaqueta()
    {
        if (PartidaTerminada)
            return;

        // Marca el final de la partida.
        PartidaTerminada = true;

        // Activa la animacion de electrocucion.
        animator.SetTrigger("ataqueRaqueta");

        // Espera a que termine la animacion.
        StartCoroutine(FinElectrocucion());
    }

    private IEnumerator FinElectrocucion()
    {
        yield return new WaitForSeconds(duracionElectrocucion);

        // Ejecuta el evento de Game Over.
        alTerminarElectrocucion.Invoke();
    }


    // ==========================================
    // ATAQUE 3: INSECTICIDA
    // ==========================================

    public void RecibirVeneno()
    {
        if (recibiendoEfecto || PartidaTerminada)
            return;

        StartCoroutine(EfectoVeneno());
    }

    private IEnumerator EfectoVeneno()
    {
        recibiendoEfecto = true;

        // Activa la animacion de mareo.
        animator.SetTrigger("nubeVeneno");

        // Reduce la velocidad al 65 por ciento.
        MultiplicadorVelocidad = multiplicadorVeneno;

        // Espera los 7 segundos.
        yield return new WaitForSeconds(duracionVeneno);

        // Recupera la velocidad normal.
        MultiplicadorVelocidad = 1f;

        // Regresa a la animacion normal.
        animator.SetTrigger("finVeneno");

        recibiendoEfecto = false;
    }
}
