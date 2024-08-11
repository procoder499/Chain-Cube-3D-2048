using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CollapsedSkipButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject CubeCollapsedPanel;
    public void OnPointerDown(PointerEventData eventData)
    {
        //SaveManager.instance.isButtonPressed = true;
        EventManager.instance.CubeCollapsedEvent();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        CubeCollapsedPanel.SetActive(false);
        SaveManager.instance.isButtonPressed = false;
    }
}

