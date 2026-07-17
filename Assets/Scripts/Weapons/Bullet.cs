using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 18f;
    [SerializeField] private float lifeTime = 3f;

    private Vector2 direction;

    public void Initialize(Vector2 shootDirection)
    {
        direction = shootDirection.normalized;

        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}