using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextLevelButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private GameObject LoseWindow;
    public void OnPointerDown(PointerEventData eventData)
    {
        SaveManager.instance.currentScore = 0;
        SaveManager.instance.Save();
        BoardManager.instance.Restart();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        LoseWindow.SetActive(false);
        SaveManager.instance.isButtonPressed = false;
    }
}
