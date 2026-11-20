using UnityEngine;

public class Moneda : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 3f;

    [Header("Animacion")]
    public float velocidadGiro = 180f;

    void Update()
    {
        // La moneda se mueve hacia la izquierda
        transform.Translate(
            Vector2.left * velocidad * Time.deltaTime,
            Space.World
        );

        // Gira la moneda
        transform.Rotate(
            Vector3.up * velocidadGiro * Time.deltaTime
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ContadorMonedas contador =
            other.GetComponentInParent<ContadorMonedas>();

        if (contador != null)
        {
            contador.AgregarMoneda();

            Destroy(gameObject);
        }
    }
}


