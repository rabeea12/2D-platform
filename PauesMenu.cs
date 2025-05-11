using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Threading.Tasks;

public class PauesMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] RectTransform pausePanelRect, pauseButtonRect,timerRect, levelTextRect;
    [SerializeField] float topPosY, middlePosY;
    [SerializeField] float tweenDuration;
    [SerializeField] CanvasGroup canvasGroup;
    

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        PausePanalIntro();
    }
    public void Home()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
    public async void Resum()
    {
        await PausePanalOutro();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    void PausePanalIntro()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.panelWhoosh);
        canvasGroup.DOFade(1, tweenDuration).SetUpdate(true);
        pausePanelRect.DOAnchorPosY(middlePosY, tweenDuration).SetUpdate(true);
        pauseButtonRect.DOAnchorPosX(150, tweenDuration).SetUpdate(true);
        timerRect.DOAnchorPosY(60, tweenDuration).SetUpdate(true);
        levelTextRect.DOAnchorPosY(60, tweenDuration).SetUpdate(true);
        
    }
    async Task PausePanalOutro()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.panelWhoosh);
        canvasGroup.DOFade(0, tweenDuration).SetUpdate(true);
        await pausePanelRect.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
        pauseButtonRect.DOAnchorPosX(-15, tweenDuration).SetUpdate(true);
        timerRect.DOAnchorPosY(-15, tweenDuration).SetUpdate(true);
        levelTextRect.DOAnchorPosY(-15, tweenDuration).SetUpdate(true);
        await Task.Delay(1000); // Wait for the outro animation to finish before returning
    }
}
