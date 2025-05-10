using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private PlayerAttack playerAttack;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    private float groundCheckRadius = 0.2f;
    private bool isGrounded;
    private bool canDoubleJump;

    [Header("Player Stats")]
    private bool isDead = false;
    public int maxHP = 10;
    private int currentHP;
    public LayerMask killLayer;
    public GameObject corpsePrefab;

    [Header("Knockback Settings")]
    public float knockbackForce = 10f;
    public float knockBackForceX = 5f;
    public float knockBackForceY = 5f;

    //private bool isDoubleJump = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAttack = GetComponent<PlayerAttack>();
        currentHP = maxHP;
    }

    void Update()
    {
        if (isDead) return;
        

        float moveInput = Input.GetAxisRaw("Horizontal");
        bool isJumpPressed = Input.GetButtonDown("Jump");

        // Movimiento horizontal
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Animaciones de movimiento
        animator.SetBool("isRunning", moveInput != 0);

        // Animaciones de salto y caída
        animator.SetBool("isJumping", !isGrounded && rb.linearVelocity.y > 0.1f);
        animator.SetBool("isFalling", !isGrounded && rb.linearVelocity.y < -0.1f);
        animator.SetBool("isGrounded", isGrounded);

      

        // Salto y doble salto
        if (isJumpPressed)
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                canDoubleJump = true;
            }
            else if (canDoubleJump)
            {
                animator.SetTrigger("dJ");
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                
                canDoubleJump = false;
            }
        }

        // Voltear sprite
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Llamar ataque

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (playerAttack != null) 
            {
                playerAttack.PerformAttack();
                animator.SetTrigger("Punch");
            }
        }

       // if (Input.GetButtonDown("Fire1"))
        //{
           // if(playerAttack != null)
           // {
                //playerAttack.PerformAttack();
                // llamamos a la animación y la animación dispara la función PerformAttack()
                // animator.SetTrigger("Punch");
           // }
        //}    
        
        // Simulaciones de daño y muerte
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(1, transform);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("isDead");
            isDead = true;
            rb.linearVelocity = Vector2.zero;
        }

        // Condición de muerte
        if(currentHP <= 0)
        {
            Die();
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, killLayer))
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        if (corpsePrefab != null)
        {
            // Instantiate the corpse prefab at the player's position
            Instantiate(corpsePrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No corpse prefab assigned to PlayerController.");
        }

        // Restart the level after 2 seconds
        Invoke("RestartLevel", 2f);

        // Delay the destruction of the player GameObject
        Destroy(gameObject, 2.1f);
    }

    void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene
            (
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
            );
    }

    public void TakeDamage(int amount, Transform attacker)
    {
        if (isDead) return;

        currentHP -= amount;
        animator.SetTrigger("isHurt");

        // Calculate knockback direction
        Vector2 knockbackDir = (transform.position.x < attacker.position.x) ? Vector2.left : Vector2.right;

        // Apply knockback force
        rb.linearVelocity = new Vector2(knockbackDir.x * knockBackForceX, knockBackForceY);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1, transform);
        }
    }

}


