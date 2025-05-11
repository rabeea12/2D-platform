using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class SimplePlayerID : MonoBehaviour
{
    public string playerID; // هذا هو الـ ID الفريد للاعب
    public TMP_Text playerIDText; // ربط هذا المتغير مع عنصر UI

    void Start()
    {
        // جعل اللعبة لا تُدمر عند تحميل مشهد جديد (اختياري)
        DontDestroyOnLoad(this.gameObject);

        // إنشاء أو استرجاع الـ ID
        GenerateOrLoadPlayerID();
    }

    void GenerateOrLoadPlayerID()
    {
        // إذا كان الـ ID موجودًا (سبق حفظه)، نسترجعه
        if (PlayerPrefs.HasKey("PlayerID"))
        {
            playerID = PlayerPrefs.GetString("PlayerID");
        }
        // إذا لا، ننشئ ID جديدًا ونحفظه
        else
        {
            playerID = Guid.NewGuid().ToString();
            PlayerPrefs.SetString("PlayerID", playerID);
            Debug.Log("تم إنشاء ID جديد: " + playerID);
        }
        ShowPlayerID();
    }

    // (اختياري) مسح الـ ID (للتجربة أو لأغراض التطوير)
    public void DeletePlayerID()
    {
        PlayerPrefs.DeleteKey("PlayerID");
        Debug.Log("تم حذف الـ ID!");
    }
    void ShowPlayerID()
    {
        playerIDText.text = "ID: " + playerID;
        
        // (اختياري) إخفاء النص بعد 3 ثواني
        Invoke("HideID", 5f);
    }
    void HideID()
    {
        if (playerIDText != null)
        {
            playerIDText.gameObject.SetActive(false);
        }
    }

    
}