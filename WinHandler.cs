using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinHandler : MonoBehaviour
{
    // [SerializeField] private GameObject winPanel;
    // [SerializeField] private Button nextLevelButton;
    // [SerializeField] private Button mainMenuButton;
    // [SerializeField] private float delayBeforeShowingPanel = 2f; // Delay before showing the win panel
    // SceneManager sceneManager; // Reference to the SceneManager
   

    // private void Awake()
    // {
    //     if (winPanel == null)
    //     {
    //         Debug.LogError("Win Panel is not assigned in the Inspector! (WinHandler)");
    //     }
    //     if (nextLevelButton == null)
    //     {
    //         Debug.LogError("Next Level Button is not assigned in the Inspector! (WinHandler)");
    //     }
    //     if (mainMenuButton == null)
    //     {
    //         Debug.LogError("Main Menu Button is not assigned in the Inspector! (WinHandler)");
    //     }
    // }

    // private void Start()
    // {
    //     // تم التعليق على هذا الجزء كما اتفقنا
    //     // if (winPanel != null)
    //     // {
    //     //     winPanel.SetActive(false);
    //     // }

    //     if (nextLevelButton != null)
    //     {
    //         nextLevelButton.onClick.AddListener(LoadNextLevel);
    //     }
    //     if (mainMenuButton != null)
    //     {
    //         mainMenuButton.onClick.AddListener(GoToMainMenu);
    //     }
    // }

    // public void ShowWinScreen()
    // {
    //     if (winPanel != null)
    //     {
    //         winPanel.SetActive(true); // تفعيل اللوحة عند الفوز
    //         Time.timeScale = 0f;     // إيقاف الوقت
    //         StartCoroutine(ShowWinScreenWithDelay()); // بدء Coroutine لتأخير إظهار اللوحة
    //     }
    // }

    // private IEnumerator ShowWinScreenWithDelay()
    // {
    //     yield return new WaitForSeconds(delayBeforeShowingPanel);
    //     if (winPanel != null)
    //     {
    //         winPanel.SetActive(true);
    //         Time.timeScale = 0f;
    //     }
    // }

    // private void LoadNextLevel()
    // {
    //     if (winPanel != null)
    //     {
    //         winPanel.SetActive(false); // **إخفاء لوحة الفوز هنا**
    //         Time.timeScale = 1f;     // استئناف الوقت قبل تحميل المشهد
    //         SceneController.instance.NextLevel(); // استخدام SceneController لتحميل المستوى التالي

    //     }
    // }

    //  public void GoToMainMenu()
    // {
    //     if (winPanel != null)
    //     {
    //         winPanel.SetActive(false); // **إخفاء لوحة الفوز هنا**
    //         Time.timeScale = 1f;     // استئناف الوقت قبل تحميل المشهد
    //         SceneController.instance.LoadMainMenu(); // استخدام SceneController لتحميل القائمة الرئيسية
    //     } 
    // }
}