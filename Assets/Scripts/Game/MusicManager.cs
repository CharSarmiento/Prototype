using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip menuTheme;
    public AudioClip gameplayTheme;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMenuTheme()
    {
        PlayMusic(menuTheme);
    }

    public void PlayGameplayTheme()
    {
        PlayMusic(gameplayTheme);
    }

    private void PlayMusic(AudioClip clip)
    {
        if (audioSource.clip == clip) return; // evitar reiniciar misma canción
        audioSource.clip = clip;
        audioSource.loop = true;
        audioSource.Play();
    }
}
