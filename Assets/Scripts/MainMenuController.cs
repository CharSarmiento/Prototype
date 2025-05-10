using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetButtonDown("Submit"))

        {
            PlayGame();
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Level01"); // Usa el nombre exacto de tu escena
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Juego cerrado");
    }
}
