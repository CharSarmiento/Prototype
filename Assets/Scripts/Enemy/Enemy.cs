using UnityEngine;

public class Enemy : MonoBehaviour
// Base class for all enemies
{
    [Header("Enemy Settings")]
    public float detectionRange = 8f;
    public float attackRange = 1.5f;
    public float moveSpeed = 2f;
    public float attackCooldown = 2f;
    public int damage = 1;

    [Header("Health Settings")]
    public int hp = 3;
    public GameObject corpsePrefab;

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
        HandleMovenetAndDetection();
    }

    protected void HandleMovenetAndDetection()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();
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

    protected void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

        animator?.SetBool("isWalking", true);

        FlipSprite(direction.x);
    }

    protected void FlipSprite(float directionX)
    {
        if (directionX > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (directionX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    public void Attack()
    {
        animator?.SetTrigger("Attack");
        // function DealDamage() will be called in the animation event
    }

    public void DealDamage()
    {
        if (player is not null && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            Player playerController = player.GetComponent<Player>();
            if (playerController is not null)
            {
                playerController.TakeDamage(damage, transform);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();

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

    private void Die()
    {
        if (corpsePrefab != null)
        {
            Instantiate(corpsePrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public void SetPlayerReference(Transform newPlayer)
    {
        this.player = newPlayer;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
