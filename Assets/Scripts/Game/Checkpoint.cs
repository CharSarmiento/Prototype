using UnityEngine;

public class Checkpoint : MonoBehaviour
{
   

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
         
            GameManager.instance.SetCheckpoint(transform.position);
            Debug.Log("Checkpoint set at: " + GameManager.instance.spawnPosition);

            Destroy(gameObject);

        }
    }
}
