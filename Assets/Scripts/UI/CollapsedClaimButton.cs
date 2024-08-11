using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CollapsedCLaimButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject CubeCollapsedPanel;
    public void OnPointerDown(PointerEventData eventData)
    {
        //SaveManager.instance.isButtonPressed = true;
        EventManager.instance.CubeCollapsedEvent_1();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        CubeCollapsedPanel.SetActive(false);
        SaveManager.instance.isButtonPressed = false;
    }
}

