using UnityEngine;
using UnityEngine.EventSystems;

public class BombAddButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool isButtonPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
        SaveManager.instance.bombCount++;
        SaveManager.instance.Save();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
    }
}
