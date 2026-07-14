using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 Move { get; private set; }

    public bool JumpPressed { get; private set; }

    public bool JumpHeld { get; private set; }

    public bool AttackPressed { get; private set; }

    private GameInput input;

    private void Awake()
    {
        input = new GameInput();

        input.Player.Move.performed += ctx => Move = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += _ => Move = Vector2.zero;

        input.Player.Jump.performed += _ =>
        {
            JumpPressed = true;
            JumpHeld = true;
        };

        input.Player.Jump.canceled += _ =>
        {
            JumpHeld = false;
        };

        input.Player.Attack.performed += _ =>
        {
            AttackPressed = true;
        };
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    private void LateUpdate()
    {
        JumpPressed = false;
        AttackPressed = false;
    }
}