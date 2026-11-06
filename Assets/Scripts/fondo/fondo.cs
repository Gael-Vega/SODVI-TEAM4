using UnityEngine;

public class fondo : MonoBehaviour
{
    public float velocidadfondo;
    [SerializeField]
    private Renderer bgRenderer;





    void Update()
    {
        bgRenderer.material.mainTextureOffset += new Vector2(velocidadfondo * Time.deltaTime, 0);

    }
}
