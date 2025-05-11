using UnityEngine;
using TMPro; // إذا كنت تستخدم TextMeshPro
using UnityEngine.UI; // إذا كنت تستخدم Text (UI)
using UnityEngine.SceneManagement;

public class LevelUIController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text levelNumberText_TMP; // إذا كنت تستخدم TextMeshPro

    private void Start()
    {
        UpdateLevelText();
    }

    private void OnEnable()
    {
        // يمكنك الاشتراك في حدث إذا كان لديك نظام إدارة مشاهد يخبرك عند تحميل مشهد جديد
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateLevelText();
    }

    private void UpdateLevelText()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex; // رقم المستوى هو فهرس المشهد الحالي + 1

        if (levelNumberText_TMP != null)
        {
            levelNumberText_TMP.text = "Level: " + currentLevel.ToString();
        }
        else
        {
            Debug.LogError("لم يتم ربط عنصر نصي لرقم المستوى في Inspector!");
        }
    }
}