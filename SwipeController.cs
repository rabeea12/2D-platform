using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
   [SerializeField] int maxPage;
   int currentPage;
   Vector3 targetPos;
   [SerializeField] RectTransform levelPagestRect;
    [SerializeField] Vector3 pageStep;
    
   [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    float dragThreshould;
    [SerializeField] Image[] barImage;
    [SerializeField] Sprite barClosed, barOpen;
    [SerializeField] Button previousBtn, nextBtn;


    public void Awake()
    {
        currentPage = 1;
        targetPos = levelPagestRect.localPosition;
        dragThreshould = Screen.width / 15;
        UpdateBar();
        UpdateArrowButton();
    }
   public void Next()
   {
         if (currentPage < maxPage)
         {
              currentPage++;
              targetPos += pageStep ;
              MovePage();
         }
        
   }
    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    
    }
    void MovePage()
    {
        levelPagestRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        UpdateBar();
        UpdateArrowButton();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
         if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x) Previous();
            else Next(); 
            
        }
        else
        {
            MovePage();
        }
    }
void UpdateBar()
{
    foreach (var item in barImage)
    {
        item.sprite = barClosed;
    }
    barImage[currentPage - 1].sprite = barOpen;
}
void UpdateArrowButton()
{
    nextBtn.interactable = true;
    previousBtn.interactable = true;
    if (currentPage == 1) previousBtn.interactable = false;
    else if (currentPage == maxPage) nextBtn.interactable = false;
}
}