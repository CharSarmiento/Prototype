using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] [Range(0f, 1f)] private float parallaxFactor = 0.5f;

    private Vector3 lastCameraPosition;

    private void Start()
    {
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 cameraDelta = cameraTransform.position - lastCameraPosition;

        transform.position += new Vector3(
            cameraDelta.x * parallaxFactor,
            cameraDelta.y * parallaxFactor,
            0f);

        lastCameraPosition = cameraTransform.position;
    }
}