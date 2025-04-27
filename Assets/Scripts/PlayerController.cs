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
    public int hP = 10;
    public LayerMask killLayer;

    private bool isDoubleJump = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        if (isDead) return;
        
        // Entrada de prueba
       if (Input.GetKeyDown(KeyCode.K))
       {
           Die();
       }

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

        // Llamar ataque

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (playerAttack != null) 
            {
                playerAttack.PerformAttack();
                animator.SetTrigger("Punch");
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if(playerAttack != null)
            {
                //playerAttack.PerformAttack();
                // llamamos a la animación y la animación dispara la función PerformAttack()
                animator.SetTrigger("Punch");
            }
        }    
        
        // Simulaciones de daño y muerte
        if (Input.GetKeyDown(KeyCode.H))
        {
            animator.SetTrigger("isHurt");
            hP--;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("isDead");
            isDead = true;
            rb.linearVelocity = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        // Verifica si está en el suelo
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        // para debug Debug.Log("Is Grounded: " + isGrounded);
        isDead = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, killLayer);
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

    void Die()
    {
       isDead = true;
       animator.SetTrigger("isDeath");
       rb.linearVelocity = Vector2.zero;
       rb.bodyType = RigidbodyType2D.Kinematic; // Opcional: congela cuerpo
       GetComponent<Collider2D>().enabled = false; // Opcional: evita colisiones

        // Si quieres reiniciar el nivel después de unos segundos
        Invoke("RestartLevel", 2f); // Espera 2 segundos
    }

    void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
            );
    }


}


