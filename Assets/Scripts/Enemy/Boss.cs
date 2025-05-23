using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Boss Settings")]
    public int maxHealth = 100;
    public GameObject bossEntranceTrigger;
    public GameObject levelCompleteTrigger;

    protected int currentHealth;
    protected bool isActive = false;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void ActivateBoss()
    {
        isActive = true;
        // Activar animaci�n o m�sica de batalla, cerrar salidas, etc.
        Debug.Log("Boss Activated!");
    }

    public virtual void TakeDamage(int amount)
    {
        if (!isActive) return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log("Boss Defeated!");
        isActive = false;
        if (levelCompleteTrigger != null)
        {
            levelCompleteTrigger.SetActive(true); // Podr�as usar esto para activar una cinem�tica o cargar siguiente nivel
        }

        Destroy(gameObject, 1f); // O reempl�zalo con una animaci�n de muerte
        //go to next level (GameManager)
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == bossEntranceTrigger)
        {
            ActivateBoss();
        }
    }
}
