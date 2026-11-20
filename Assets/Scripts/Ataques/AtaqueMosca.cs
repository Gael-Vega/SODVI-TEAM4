
using UnityEngine;

public class AtaqueMosca : MonoBehaviour
{
    // Elegimos el tipo de ataque desde el Inspector.
    public enum TipoAtaque
    {
        Periodico,
        Raqueta,
        Veneno
    }

    [Header("Tipo de ataque")]
    public TipoAtaque tipoAtaque;

    [Header("Configuracion")]
    public bool destruirAlImpactar = true;

    private bool yaImpacto = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Buscamos el componente de efectos en el objeto que recibio el golpe.
        EfectosMosca efectos = other.GetComponent<EfectosMosca>();

        // Si no esta directamente en el objeto, buscamos en su objeto padre.
        if (efectos == null)
        {
            efectos = other.GetComponentInParent<EfectosMosca>();
        }

        // Si no encontramos a la mosca, no hacemos nada.
        if (efectos == null || yaImpacto)
        {
            return;
        }

        yaImpacto = true;

        // Ejecutamos el efecto correspondiente al ataque.
        switch (tipoAtaque)
        {
            case TipoAtaque.Periodico:
                efectos.RecibirPeriodico();
                break;

            case TipoAtaque.Raqueta:
                efectos.RecibirRaqueta();
                break;

            case TipoAtaque.Veneno:
                efectos.RecibirVeneno();
                break;
        }

        // Destruimos el objeto de ataque despues del impacto.
        if (destruirAlImpactar)
        {
            Destroy(gameObject);
        }
    }
}



