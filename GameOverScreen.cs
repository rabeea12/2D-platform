using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private string mainMenuSceneName = "MainMenu"; // اسم مشهد القائمة الرئيسية

    private void Awake()
    {
        if (gameOverPanel == null)
        {
            Debug.LogError("Game Over Panel غير مربوط في Inspector!");
        }
        if (mainMenuButton == null)
        {
            Debug.LogError("Main Menu Button غير مربوط في Inspector!");
        }
    }

    private void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); // إخفاء اللوحة عند البدء
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(LoadMainMenu);
        }
    }

    public void ShowGameOverScreen()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f; // إيقاف اللعبة مؤقتًا
        }
        else
        {
            Debug.LogError("Game Over Panel غير موجود!");
        }
    }

    private void LoadMainMenu()
    {
        Time.timeScale = 1f; // استئناف الوقت
        SceneManager.LoadScene(mainMenuSceneName);
    }
}