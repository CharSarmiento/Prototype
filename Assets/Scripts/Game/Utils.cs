using UnityEngine;

public class Utils : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    public float scrollLimit = 18f;
    public float scrollStart = -18f;

    void Update()
    {
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        if (transform.position.x < scrollStart)
        {
            transform.position = new Vector3(scrollLimit, transform.position.y, transform.position.z);
        }
        
    }
}
