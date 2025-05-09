using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public int damage = 1;
    public int hp = 3;

    private GameObject player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeDamage(damage, transform);
            }
        }
                
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    

}
