using UnityEngine;
using UnityEngine.EventSystems;

public class JokerAddButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool isButtonPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
        SaveManager.instance.jokerCount++;
        SaveManager.instance.Save();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
    }
}
