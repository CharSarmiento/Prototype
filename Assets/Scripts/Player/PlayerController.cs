using UnityEngine;
using UnityEngine.InputSystem;

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

    private Rigidbody2D rb;
    private GameInput input;

    private Vector2 moveInput;
    private bool jumpPressed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        input = new GameInput();

        input.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += _ => moveInput = Vector2.zero;

        input.Player.Jump.performed += _ => jumpPressed = true;
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void Update()
    {
        Flip();

        if (jumpPressed && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpPressed = false;
        }

        jumpPressed = false;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void Flip()
    {
        if (moveInput.x > 0)
            transform.localScale = Vector3.one;

        else if (moveInput.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer);
    }
}