using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManagerInstance; // احتفظ بنسخة محلية عشان نتجنب الوصول للـ static property كتير

    void Start()
    {
        // حاول الحصول على نسخة AudioManager عند بدء تشغيل السكريبت
        audioManagerInstance = AudioManager.instance;
        if (audioManagerInstance == null)
        {
            Debug.LogError("AudioManager instance غير موجود في مشهد MainMenu!");
        }
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

    public void PlayGame()
    {
        PlayButtonClickSound();
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        PlayButtonClickSound();
        Application.Quit();
        Debug.Log("QUIT!");
    }
}