using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput playerInput;
    private Animator animator;

    private static readonly int AttackHash = Animator.StringToHash("Attack");
    [SerializeField] private float attackDuration = 0.5f;
    public bool IsAttacking { get; private set; }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (playerInput.AttackPressed && !IsAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        IsAttacking = true;
        animator.SetTrigger(AttackHash);

        StopAllCoroutines();
        StartCoroutine(AttackRoutine());
    }

    public void EndAttack()
    {
        IsAttacking = false;
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(attackDuration);

        IsAttacking = false;
    }
}