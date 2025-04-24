using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    private float groundCheckRadius = 0.2f;
    private bool isGrounded;
    private bool canDoubleJump;

    private bool isDead = false;
    private bool isDoubleJump = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isDead) return;

        float moveInput = Input.GetAxisRaw("Horizontal");
        bool isJumpPressed = Input.GetButtonDown("Jump");

        // Movimiento horizontal
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Animaciones de movimiento
        animator.SetBool("isRuning", moveInput != 0);

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

        // Simulaciones de daño y muerte
        if (Input.GetKeyDown(KeyCode.H))
            animator.SetTrigger("isHurt");

        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("isDeath");
            isDead = true;
            rb.linearVelocity = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        // Verifica si está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        Debug.Log("Is Grounded: " + isGrounded);
    }

    void OnDrawGizmos()
    {
        // Muestra el área de detección de suelo en la vista de escena
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
