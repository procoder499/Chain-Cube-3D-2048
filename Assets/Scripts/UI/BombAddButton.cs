using UnityEngine;
using UnityEngine.EventSystems;

public class BombAddButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        SaveManager.instance.isButtonPressed = true;
        SaveManager.instance.bombCount++;
        SaveManager.instance.Save();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SaveManager.instance.isButtonPressed = false;
    }
}
