using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    // [SerializeField] private WinHandler winHandler; // Reference to WinHandler

    private void Awake()
    {
        // if (winHandler == null)
        // {
        //     Debug.LogError("WinHandler is not assigned in the Inspector! (FinishPoint)");
        // }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered FinishPoint");
           // winHandler?.ShowWinScreen(); // استدعاء دالة إظهار شاشة الفوز

            UnlockNewLevel();
            // يمكنك التعليق على استدعاء NextLevel هنا إذا كنت تريد أن تبقى شاشة الفوز معروضة
            SceneController.instance.NextLevel();
        }
    }
    void UnlockNewLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel",1) + 1);
            PlayerPrefs.Save();
        }
    }
}