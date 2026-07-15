using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed = 8f;

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z);

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            followSpeed * Time.deltaTime);
    }
}