using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [Header("Audio Mixer References")]
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
     [Header("Volume Parameters")]
    [SerializeField] private float defaultVolume = 0.75f; // حجم الصوت الافتراضي
    private const string MUSIC_VOLUME_KEY = "musicVolume";
    private const string SFX_VOLUME_KEY = "sfxVolume";
    private AudioManager audioManagerInstance; // احتفظ بنسخة محلية عشان نتجنب الوصول للـ static property كتير

    private void Start()
    {
        audioManagerInstance = AudioManager.instance;
        InitializeSliders();
        LoadVolumeSettings();
    }
    private void LoadVolumeSettings()
    {
        // تحميل إعدادات الموسيقى
        musicSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, defaultVolume);
        SetMusicVolume(); // تطبيق القيمة مباشرة
        
        // تحميل إعدادات المؤثرات الصوتية
        sfxSlider.value = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, defaultVolume);
        SetSFXVolume(); // تطبيق القيمة مباشرة
    }
     private void InitializeSliders()
    {
        // ضبط القيم الدنيا والقصوى للشرائح
        musicSlider.minValue = 0.0001f; // القيمة الدنيا لتجنب اللوغاريتم صفر
        musicSlider.maxValue = 1f;
        sfxSlider.minValue = 0.0001f;
        sfxSlider.maxValue = 1f;
    }
    public void PlayButtonClickSound()
    {
        if (audioManagerInstance != null)
        {
            audioManagerInstance.PlayButtonClickSound();
        }
        else
        {
            Debug.LogError("AudioManager instance غير موجود لتشغيل صوت النقر!");
        }
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume); // Save the music volume
        
    }
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        myMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume); // Save the SFX volume
    }
     private float ConvertToDecibel(float volume)
    {
        // تحويل القيمة الخطية إلى ديسيبل (لوغاريتمي)
        return Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20f;
    }

    public void ResetToDefault()
    {
        // إعادة الضبط إلى القيم الافتراضية
        musicSlider.value = defaultVolume;
        sfxSlider.value = defaultVolume;
        SetMusicVolume();
        SetSFXVolume();
    }
    // public void LoadVolume()
    // {
    //     musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
    //     sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume"); // Default to 0.5 if not set
    //     SetMusicVolume();
    // }
    
}
