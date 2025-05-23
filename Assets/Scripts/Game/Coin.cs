using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1; // Valor de la moneda

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.AddCoins(value);
            Destroy(gameObject);
        }
    }
}
