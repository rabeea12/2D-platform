using UnityEngine;

public class TutorialController : MonoBehaviour
{
    private const string TutorialCompletedKey = "TutorialCompleted";

    void Start()
    {
        // Check if tutorial has been completed before
        if (PlayerPrefs.GetInt(TutorialCompletedKey, 0) == 1)
        {
            // Tutorial already completed, disable or skip tutorial logic here
            gameObject.SetActive(false);
        }
        else
        {
            // Show tutorial
            gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Call this method when the player checks "right" to mark tutorial as completed
    /// </summary>
    public void SetTutorialCompleted()
    {
        PlayerPrefs.SetInt(TutorialCompletedKey, 1);
        PlayerPrefs.Save();
        // Optionally disable tutorial UI or this controller
        gameObject.SetActive(false);
    }
}
