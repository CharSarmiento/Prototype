using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private float lengthX;
    private float lengthY;
    public GameObject cam;
    public Vector2 parallaxEffect; // Ahora es un Vector2: (X, Y)

    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void FixedUpdate()
    {
        float tempX = cam.transform.position.x * (1 - parallaxEffect.x);
        float distX = cam.transform.position.x * parallaxEffect.x;

        float tempY = cam.transform.position.y * (1 - parallaxEffect.y);
        float distY = cam.transform.position.y * parallaxEffect.y;

        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);

        // Repetir en X
        if (tempX > startPosX + (lengthX / 2)) startPosX += lengthX;
        else if (tempX < startPosX - (lengthX / 2)) startPosX -= lengthX;

        // Repetir en Y (opcional, depende si quieres un fondo que se repita verticalmente también)
        //if (tempY > startPosY + (lengthY / 2)) startPosY += lengthY;
        //else if (tempY < startPosY - (lengthY / 2)) startPosY -= lengthY;
    }
}
