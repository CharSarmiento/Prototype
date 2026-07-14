using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private float jumpCutMultiplier = 0.5f;
    [SerializeField] private float fallGravityMultiplier = 2.5f;
    [SerializeField] private float maxFallSpeed = 18f;


    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private Animator animator;


     private static readonly int SpeedHash = Animator.StringToHash("Speed");
     private static readonly int GroundedHash = Animator.StringToHash("Grounded");
     private static readonly int VerticalSpeedHash = Animator.StringToHash("VerticalSpeed");

    private void Awake()
    {

        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
  
    }

    private void Update()
    {
        animator.SetFloat(SpeedHash, Mathf.Abs(playerInput.Move.x));
        animator.SetBool(GroundedHash, IsGrounded());
        animator.SetFloat(VerticalSpeedHash, rb.linearVelocity.y);

        Flip();

        if (playerInput.JumpPressed && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (!playerInput.JumpHeld && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x, 
                rb.linearVelocity.y * jumpCutMultiplier);
        }

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(playerInput.Move.x * moveSpeed, rb.linearVelocity.y);

        ApplyBetterJump();
    }

    private void Flip()
    {
        if (playerInput.Move.x > 0)
            transform.localScale = Vector3.one;

        else if (playerInput.Move.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void ApplyBetterJump()
{
    if (rb.linearVelocity.y < 0f)
    {
        rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallGravityMultiplier - 1f) * Time.fixedDeltaTime;

        if (rb.linearVelocity.y < -maxFallSpeed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -maxFallSpeed);
        }
    }
}

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            groundCheck.position,
            groundCheckRadius);
    }

   
}