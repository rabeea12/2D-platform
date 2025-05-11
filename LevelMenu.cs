using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButton; // الأب اللي بيحتوي على الصفحات

    private void Awake()
    {
        ButtonsToArray();
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void OpenLevel(int levelId)
    {
        string levelName = "Level" + levelId;
        SceneManager.LoadScene(levelName);
    }

    void ButtonsToArray()
    {
        List<Button> allButtons = new List<Button>();
        int levelNumberCounter = 1;

        // تصفح كل الصفحات اللي هي أبناء الـ levelButton
        for (int pageIndex = 0; pageIndex < levelButton.transform.childCount; pageIndex++)
        {
            Transform pageTransform = levelButton.transform.GetChild(pageIndex);

            // تصفح كل الأزرار اللي هي أبناء الصفحة الحالية
            for (int buttonIndex = 0; buttonIndex < pageTransform.childCount; buttonIndex++)
            {
                Transform buttonTransform = pageTransform.GetChild(buttonIndex);
                Button button = buttonTransform.GetComponent<Button>();
                if (button != null)
                {
                    allButtons.Add(button);

                    // الوصول لمكون النص وعرض رقم المستوى المتسلسل
                    TextMeshProUGUI buttonText_TMP = buttonTransform.GetComponentInChildren<TextMeshProUGUI>();
                    Text buttonText_Legacy = buttonTransform.GetComponentInChildren<Text>();

                    if (buttonText_TMP != null)
                    {
                        buttonText_TMP.text = levelNumberCounter.ToString();
                    }
                    else if (buttonText_Legacy != null)
                    {
                        buttonText_Legacy.text = levelNumberCounter.ToString();
                    }
                    else
                    {
                        Debug.LogWarning("لا يوجد مكون Text أو TextMeshProUGUI في الزر " + buttonTransform.name);
                    }
                    levelNumberCounter++;
                }
            }
        }
        buttons = allButtons.ToArray();
    }
}