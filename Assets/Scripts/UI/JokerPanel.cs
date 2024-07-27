using UnityEngine;
using UnityEngine.EventSystems;

public class JokerPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool isButtonPressed = false;
    public ControlMovement mainCube;

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
        if (SaveManager.instance.jokerCount >= 1) SaveManager.instance.jokerCount--;
        SaveManager.instance.Save();
        mainCube.mainCube.gameObject.SetActive(false);
        mainCube.mainCube = CubeSpawnController.Instance.SpawnJoker();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
    }
}
