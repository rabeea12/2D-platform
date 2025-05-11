using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LoseHandler : MonoBehaviour
{
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button watchAdButton; // زر مشاهدة الإعلان
    SceneManager sceneManager; // Reference to the SceneManager


    private void Awake()
    {

    }

    private void Start()
    {
        // if (losePanel != null)
        // {
        //     losePanel.SetActive(false); // Hide the lose panel at the start
        // }

        if (retryButton != null)
        {
            retryButton.onClick.AddListener(RetryLevel);
        }
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        }
        
    }

    public void ShowLoseScreen()
    {
        Time.timeScale = 0f; // Pause the game
        if (losePanel != null)
        {
            losePanel.SetActive(true); // Show the lose panel
        }
    }
    private void RetryLevel()
    {
        if (losePanel != null)
        {
            losePanel.SetActive(false); // Hide the lose panel
            Time.timeScale = 1f; // Resume time before loading the scene
            SceneController.instance.RetryLevel(); // Use SceneController to reload the level
        }
    }
     public void GoToMainMenu()
    {
        if (losePanel != null)
        {
            losePanel.SetActive(false); // **إخفاء لوحة الفوز هنا**
            Time.timeScale = 1f;     // استئناف الوقت قبل تحميل المشهد
            SceneController.instance.LoadMainMenu(); // استخدام SceneController لتحميل القائمة الرئيسية
        } 
    }
   

}