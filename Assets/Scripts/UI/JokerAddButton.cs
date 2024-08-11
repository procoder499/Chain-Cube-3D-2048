using UnityEngine;
using UnityEngine.EventSystems;

public class JokerAddButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        SaveManager.instance.isButtonPressed = true;
        SaveManager.instance.jokerCount++;
        SaveManager.instance.Save();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SaveManager.instance.isButtonPressed = false;
    }
}
