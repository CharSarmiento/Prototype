using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Text livesText;

    void Update()
    {
        livesText.text = "Vidas: " + GameManager.instance.lives;
    }
}
