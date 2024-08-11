using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RestartButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject SettingWindow;

    public void OnPointerDown(PointerEventData eventData)
    {
        SaveManager.instance.currentScore = 0;
        SaveManager.instance.Save();
        BoardManager.instance.Restart();
        SaveManager.instance.isButtonPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SaveManager.instance.isButtonPressed = false;
        SettingWindow.SetActive(false);
    }

}
