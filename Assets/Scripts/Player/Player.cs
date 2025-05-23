using System.Xml.Serialization;
using UnityEngine;

public class Player : MonoBehaviour
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

        HandleMovementInput();
        HandleJumpInput();
        UpdateAnimator();

    }

    void FixedUpdate() 
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);// ¿Está en el suelo?

        if (Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, killLayer)) // Muere al caer (kill Layer)
        {
            Die();
        }
    }

    private void HandleMovementInput()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        animator.SetBool("isRunning", moveInput != 0);

        // Voltear sprite
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }



    public void HandleJumpInput()
    {
        if (Input.GetButtonDown("Jump"))
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

    }

    private void UpdateAnimator()
    {

        animator.SetBool("isJumping", !isGrounded && rb.linearVelocity.y > 0.1f);
        animator.SetBool("isFalling", !isGrounded && rb.linearVelocity.y < -0.1f);
        animator.SetBool("isGrounded", isGrounded);

    }

    public void Die()
    {
        isDead = true;
        
        if (corpsePrefab is not null)
        {
            Instantiate(corpsePrefab, transform.position, Quaternion.identity);
            gameObject.SetActive(false); // Desactiva el jugador
            GameManager.instance.PlayerDied();
           
        }
        else
        {
            Debug.LogWarning("No corpse prefab assigned to PlayerController.");
        }
        
    }

    void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene
            (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
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
            Debug.Log("Player is dead");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1, transform);
        }
    }

}


