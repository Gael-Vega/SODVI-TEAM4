using UnityEngine;
using TMPro;

public class ContadorMonedas : MonoBehaviour
{
    public int monedas = 0;

    public TMP_Text textoMonedas;

    void Start()
    {
        ActualizarTexto();
    }

    public void AgregarMoneda()
    {
        monedas++;

        ActualizarTexto();
    }

    void ActualizarTexto()
    {
        if (textoMonedas != null)
        {
            textoMonedas.text = monedas.ToString();
        }
    }
}


