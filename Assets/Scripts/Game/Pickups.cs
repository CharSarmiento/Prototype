using UnityEngine;

public class Pickups : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float flipRate = 1f; // Controls the speed of the rotation effect

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        SimulateRotation(flipRate);
    }

    public void SimulateRotation(float speed = 1f)
    {
        float scaleX = Mathf.Lerp(-1f, 1f, Mathf.PingPong(Time.time * speed, 1f));
        spriteRenderer.transform.localScale = new Vector3
            (scaleX, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.z); 

    }
}
