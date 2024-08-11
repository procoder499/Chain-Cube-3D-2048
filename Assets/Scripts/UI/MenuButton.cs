using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject SettingWindow;
    public void OnPointerDown(PointerEventData eventData)
    {
        SaveManager.instance.isButtonPressed = true;
        SettingWindow.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
       // SaveManager.instance.isButtonPressed = false;
    }
}
