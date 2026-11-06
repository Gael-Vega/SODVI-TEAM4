using Unity.Tutorials.Editor.Paragraphs;
using UnityEngine;


public class Movimiento : MonoBehaviour
{

    public float velocidad = 5;
    private Rigidbody2D rb2D;

    private float movimiento;
    private float movimientovertical;

  




    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {

        movimiento = Input.GetAxisRaw("Horizontal");
        rb2D.linearVelocity = new Vector2(movimiento * velocidad, rb2D.linearVelocity.y);

        if (movimiento != 0)
            transform.localScale = new Vector3(Mathf.Sign(movimiento), 1, 1);


        movimientovertical = Input.GetAxisRaw("Vertical");
        rb2D.linearVelocity = new Vector2(rb2D.linearVelocity.x, movimientovertical * velocidad);

        

        // Controles invertidos

        // movimiento = Input.GetAxisRaw("Horizontal");
        // rb2D.linearVelocity = new Vector2(movimiento * velocidad, rb2D.linearVelocity.y);
        //
        //
        //movimientovertical = Input.GetAxisRaw("Vertical");
        //rb2D.linearVelocity = new Vector2(movimientovertical * velocidad, rb2D.linearVelocity.x);




    }
}
