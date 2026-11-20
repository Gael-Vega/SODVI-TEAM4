using UnityEngine;

public class GeneradorMonedas : MonoBehaviour
{
    [Header("Moneda")]
    public GameObject prefabMoneda;

    [Header("Tiempo de aparicion")]
    public float tiempoMinimo = 2f;
    public float tiempoMaximo = 4f;

    [Header("Altura")]
    public float margenVertical = 1f;

    private float tiempoSiguiente;

    private float limiteDerecho;
    private float limiteInferior;
    private float limiteSuperior;

    void Start()
    {
        CalcularLimites();
        ProgramarSiguienteMoneda();
    }

    void Update()
    {
        tiempoSiguiente -= Time.deltaTime;

        if (tiempoSiguiente <= 0)
        {
            CrearMoneda();
            ProgramarSiguienteMoneda();
        }
    }

    void CalcularLimites()
    {
        Camera camara = Camera.main;

        Vector3 esquina =
            camara.ViewportToWorldPoint(
                new Vector3(1, 1, 10)
            );

        limiteDerecho = esquina.x;

        Vector3 inferior =
            camara.ViewportToWorldPoint(
                new Vector3(0, 0, 10)
            );

        limiteInferior = inferior.y;

        limiteSuperior = esquina.y;
    }

    void ProgramarSiguienteMoneda()
    {
        tiempoSiguiente = Random.Range(
            tiempoMinimo,
            tiempoMaximo
        );
    }

    void CrearMoneda()
    {
        float altura = Random.Range(
            limiteInferior + margenVertical,
            limiteSuperior - margenVertical
        );

        Vector3 posicion = new Vector3(
            limiteDerecho + 1f,
            altura,
            0
        );

        Instantiate(
            prefabMoneda,
            posicion,
            Quaternion.identity
        );
    }
}


