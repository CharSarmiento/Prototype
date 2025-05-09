using UnityEngine;

public class BrigandEnemy : MonoBehaviour
{
    public float detectionRange = 8f;
    public float attackRange = 1.5f;
    public float moveSpeed = 2f;
    public float attackCooldown = 2f;
    public int damage = 1;

    private Transform player;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
        {
            // Mover hacia el jugador
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

            animator?.SetBool("isWalking", true);

            // Voltear sprite
            FlipSprite(direction.x);
        }
        else
        {
            animator?.SetBool("isWalking", false);
        }

        if (distanceToPlayer <= attackRange && Time.time - lastAttackTime > attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    void FlipSprite(float directionX)
    {
        if (directionX > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (directionX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void Attack()
    {
        animator?.SetTrigger("Attack");
        // Aquí puedes llamar a una función para aplicar daño si el jugador está en rango.
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void DealDamage()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage(damage, transform);
            }
        }
    }


}
