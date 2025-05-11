using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;
    // [SerializeField] GameObject childToActivate;
    // [SerializeField] bool activateChildOnTransition = false;
    
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

    public void NextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1f); //  السالم الانتظار حتى تنتهي الرسوم المتحركةنماء الخيرية صباح 
        // if (activateChildOnTransition && childToActivate != null)
        // {
        //     childToActivate.SetActive(true);
        // }
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            transitionAnim.SetTrigger("Start");
        //     if (activateChildOnTransition && childToActivate != null)
        // {
        //     childToActivate.SetActive(false);
        // }
        }
    }
    public void LoadMainMenu()
    {
        StartCoroutine(LoadMainMenuScene());
    }
    IEnumerator LoadMainMenuScene()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1f); //  السالم الانتظار حتى تنتهي الرسوم المتحركةنماء الخيرية صباح 

        // if (activateChildOnTransition && childToActivate != null)
        // {
        //     childToActivate.SetActive(true);
        // }
        SceneManager.LoadScene("MainMenu");
        transitionAnim.SetTrigger("Start");
        // if (activateChildOnTransition && childToActivate != null)
        // {
        //     childToActivate.SetActive(false);
        // }
    }
     public void RetryLevel()
    {
        StartCoroutine(ReloadLevel());
    }
    IEnumerator ReloadLevel()
    {
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1f); //  السالم الانتظار حتى تنتهي الرسوم المتحركةنماء الخيرية صباح 
        // if (activateChildOnTransition && childToActivate != null)
        // {
        //     childToActivate.SetActive(true);
        // }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            GameController gameController = player.GetComponent<GameController>();
            if (gameController != null)
            {
                gameController.RespawnPlayer();
            }
        }
        transitionAnim.SetTrigger("Start");
        // if (activateChildOnTransition && childToActivate != null)
        // {
        //     childToActivate.SetActive(false);
        // }
    }

    // public void ActivateChild()
    // {
    //     if (childToActivate != null)
    //     {
    //         childToActivate.SetActive(true);
    //     }
    // }
    // public void DeactivateChild()
    // {
    //     if (childToActivate != null)
    //     {
    //         childToActivate.SetActive(false);
    //     }
    // }
}
