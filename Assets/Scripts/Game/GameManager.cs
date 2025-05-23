using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int coinCount = 0;

    public int lives = 3;
    public Vector3 spawnPosition;
    public Vector3 firstSpawn;
    public GameObject playerPrefab;
        


    void Start()
    {

        spawnPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        firstSpawn = spawnPosition;
        AudioManager.Instance.PlayGameplayTheme();

    }

    void Awake()
    {
        if (instance is null)
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

    public void RestartScene(float delay = 0f)
    {
        if (delay > 0f)
        {
            Invoke("RestartLevel", delay);
        }
        else
        {
            RestartLevel();
        }
       
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Reiniciando nivel...");
    }
    
    public void PlayerDied()
    {
        lives--;
        Debug.Log("Vidas restantes: " + lives);
        if (lives < 0)
        {
            Debug.Log("Game Over");
            GameOver();
        }
        else
        {
            RespawnPlayer(); 
            Debug.Log("Reapareciendo jugador...");
        }
    }
    
    
    public void RespawnPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player is not null) // Si el jugador ya existe, lo reutilizamos
        {
            /*player.SetActive(true);
            player.transform.position = spawnPosition;
            Debug.Log("Reapareciendo jugador en: " + spawnPosition);*/
        }
        else if (playerPrefab is not null) // Como por alguna razon el jugador no existe, lo instanciamos 
        {
            player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Instanciando jugador en: " + spawnPosition);
        }
        else
        {
            Debug.LogWarning("No se encontró el objeto del jugador para reaparecer.");
        }

        //Ahora actualizamos VirtualCamera y enemigos para que sigan al jugador
        var virtualCamera = Object.FindFirstObjectByType<Unity.Cinemachine.CinemachineCamera>();
        if (virtualCamera is not null)
        {
            virtualCamera.Follow = player.transform;
        }

        foreach (var enemy in Object.FindObjectsByType<Enemy>(FindObjectsSortMode.None))
        {
            enemy.SetPlayerReference(player.transform);
        }   
        
    }

    public void SetCheckpoint(Vector3 position)
    {
        spawnPosition = position;
        Debug.Log("Nuevo checkpoint establecido: " + position);
    }

    void GameOver ()
    {
        Debug.Log("Game Over");
        lives = 3;
        spawnPosition = firstSpawn;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    void Update()
    {
      Debug.Log($"Posición del jugador: {spawnPosition}");
    }
}
