using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerInput playerInput;
    private Animator animator;
    [SerializeField] private GameObject bulletPrefab;   
    [SerializeField] private Transform firePoint;

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

        Shoot();

        StopAllCoroutines();
        StartCoroutine(AttackRoutine());
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(
         bulletPrefab,
         firePoint.position,
         Quaternion.identity);

        Vector2 direction = transform.localScale.x > 0
         ? Vector2.right
         : Vector2.left;

        bullet.GetComponent<Bullet>().Initialize(direction);
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