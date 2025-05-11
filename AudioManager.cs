using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------Audio Sources-----")]
    [SerializeField]  AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("------Audio Clip -----")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip checkpoint;
    public AudioClip wallTouch;
    public AudioClip portalIn;
    public AudioClip portalOut;   
    public AudioClip buttonClick;
    public AudioClip buttonWhoosh;  
    public AudioClip panelWhoosh;
    public AudioClip PlayButton;  

    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;

        }
    }

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void PlayButtonClickSound()
    {
        if (buttonClick != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(buttonClick);
        }
        else
        {
            Debug.LogWarning("Button Click Audio Clip أو SFX Audio Source غير مربوط في AudioManager!");
        }
    }
    
}
