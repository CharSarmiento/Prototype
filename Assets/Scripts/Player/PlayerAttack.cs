using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Hitbox Settings")]
    public Collider2D hitbox;           // El collider que detecta los enemigos
    public float hitDuration = 0.1f;    // Cu�nto dura activo el golpe

    [Header("Attack Settings")]
    public int attackDamage = 1;         // Cu�nto da�o hace el golpe
    public LayerMask enemyLayers;        // Qu� layers son enemigos

    private void Start()
    {
        if (hitbox != null)
            hitbox.enabled = false; // Asegurarse de que al inicio est� desactivado
    }

    // Esta funci�n ser� llamada por el Animation Event
    public void PerformAttack()
    {
        Debug.Log("�Ataque realizado!");

        if (hitbox != null)
        {
            hitbox.enabled = true;
            Invoke(nameof(EndAttack), hitDuration);

            // Detectar enemigos en el �rea del hitbox
            DetectAndDamageEnemies();
        }
    }

    // Desactiva el golpe despu�s de un peque�o tiempo
    private void EndAttack()
    {
        if (hitbox != null)
            hitbox.enabled = false;
    }

    // Detectar y aplicar da�o a los enemigos
    private void DetectAndDamageEnemies()
    {
        // Preparamos un array para guardar los resultados
        Collider2D[] results = new Collider2D[10];

        // Creamos el filtro
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(enemyLayers);
        filter.useLayerMask = true;

        // Usamos OverlapCollider, que devuelve la cantidad de colliders encontrados
        int hitCount = hitbox.Overlap(filter, results);

        // Recorremos solo los resultados v�lidos
        for (int i = 0; i < hitCount; i++)
        {
            Collider2D enemy = results[i];
            if (enemy != null)
            {
                Debug.Log($"Golpeado: {enemy.name}");

                // Llamamos a TakeDamage si el enemigo tiene EnemyHealth
                // enemy.GetComponent<EnemyHealth>()?.TakeDamage(attackDamage);
            }
        }
    }
}
