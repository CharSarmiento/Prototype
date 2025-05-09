using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int coinCount = 0;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // No destruir este objeto al cargar una nueva escena
        }
        else
        {
            Destroy(gameObject); // Destruir el objeto duplicado    
        }
    }

    public void AddCoins (int amount)
    {
        coinCount += amount;
        Debug.Log("Coins: " + coinCount);
    }

    public void RemoveCoins (int amount) 
    {
        coinCount -= amount;
        Debug.Log("Coins: " + coinCount);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
