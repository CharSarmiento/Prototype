using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private float lengthX;
    private float lengthY;
    public GameObject cam;
    public Vector2 parallaxEffect; // (X, Y)

    void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
        lengthX = GetComponent<SpriteRenderer>().bounds.size.x;
        lengthY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void LateUpdate()
    {
        // Calculate parallax effect
        float distX = cam.transform.position.x * parallaxEffect.x;
        float distY = cam.transform.position.y * parallaxEffect.y;

        // Update position with parallax effect
        transform.position = new Vector3(startPosX + distX, startPosY + distY, transform.position.z);

        // Handle infinite scrolling in X
        float tempX = cam.transform.position.x * (1 - parallaxEffect.x);
        if (tempX > startPosX + (lengthX / 2)) startPosX += lengthX;
        else if (tempX < startPosX - (lengthX / 2)) startPosX -= lengthX;

        // Optional: Handle infinite scrolling in Y
        // float tempY = cam.transform.position.y * (1 - parallaxEffect.y);
        // if (tempY > startPosY + (lengthY / 2)) startPosY += lengthY;
        // else if (tempY < startPosY - (lengthY / 2)) startPosY -= lengthY;
    }
}
