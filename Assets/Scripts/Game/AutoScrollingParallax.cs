using UnityEngine;
using UnityEngine.UIElements;

public class AutoScrollingParallax : MonoBehaviour
{
    public float scrollSpeed = 0.5f; 
    public Renderer backgroundRenderer;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Time.time * scrollSpeed;
        backgroundRenderer.material.mainTextureOffset = new Vector2(x, 0);

        
    }
}
