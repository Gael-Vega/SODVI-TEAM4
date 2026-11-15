//using Unity.Tutorials.Editor.Paragraphs;
//using UnityEngine;


//public class Movimiento : MonoBehaviour
//{

//public float velocidad = 5;
//private Rigidbody2D rb2D;

//private float movimiento;
//private float movimientovertical;






//void Start()
//{
//  rb2D = GetComponent<Rigidbody2D>();


//}

// Update is called once per frame
//void Update()
//{

//  movimiento = Input.GetAxisRaw("Horizontal");
//rb2D.linearVelocity = new Vector2(movimiento * velocidad, rb2D.linearVelocity.y);

//if (movimiento != 0)
//  transform.localScale = new Vector3(Mathf.Sign(movimiento), 1, 1);


//movimientovertical = Input.GetAxisRaw("Vertical");
//rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, movimientovertical * velocidad);



// Controles invertidos

// movimiento = Input.GetAxisRaw("Horizontal");
// rb2D.linearVelocity = new Vector2(movimiento * velocidad, rb2D.linearVelocity.y);
//
//
//movimientovertical = Input.GetAxisRaw("Vertical");
//rb2D.linearVelocity = new Vector2(movimientovertical * velocidad, rb2D.linearVelocity.x);


// }
//}






using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [Header("Configuracion del movimiento")]
    public float velocidad = 5f;

    private Rigidbody2D rb2D;

    private float movimiento;
    private float movimientovertical;

    // Referencia al script de efectos de la mosca.
    private EfectosMosca efectos;


    void Start()
    {
        // Obtenemos el Rigidbody2D de la mosca.
        rb2D = GetComponent<Rigidbody2D>();

        // Obtenemos el script que controla los ataques.
        efectos = GetComponent<EfectosMosca>();
    }


    void Update()
    {
        // Leemos los controles WASD.
        movimiento = Input.GetAxisRaw("Horizontal");
        movimientovertical = Input.GetAxisRaw("Vertical");

        // Invertimos los controles cuando la mosca
        // recibe el impacto del periodico.
        if (efectos != null && efectos.ControlesInvertidos)
        {
            movimiento = -movimiento;
            movimientovertical = -movimientovertical;
        }

        // Cambiamos la orientacion de la mosca.
        if (movimiento != 0)
        {
            transform.localScale = new Vector3(
                Mathf.Sign(movimiento),
                1,
                1
            );
        }
    }


    void FixedUpdate()
    {
        // Si la partida termino, detenemos el movimiento.
        if (efectos != null && efectos.PartidaTerminada)
        {
            rb2D.linearVelocity = Vector2.zero;
            return;
        }

        // Creamos la direccion del movimiento.
        Vector2 direccion = new Vector2(
            movimiento,
            movimientovertical
        );

        // Evitamos que el movimiento diagonal sea mas rapido.
        direccion = direccion.normalized;

        // Velocidad normal de la mosca.
        float multiplicador = 1f;

        // Consultamos si algun ataque modifica la velocidad.
        if (efectos != null)
        {
            multiplicador = efectos.MultiplicadorVelocidad;
        }

        // Aplicamos el movimiento.
        rb2D.linearVelocity = direccion * velocidad * multiplicador;
    }
}
