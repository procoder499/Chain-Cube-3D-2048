using UnityEngine;
using UnityEngine.EventSystems;

public class BombPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool isButtonPressed = false;
    public ControlMovement mainCube;

    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
        if(SaveManager.instance.bombCount>=1) SaveManager.instance.bombCount--;
        SaveManager.instance.Save();
        mainCube.mainCube.gameObject.SetActive(false);
        mainCube.mainCube = CubeSpawnController.Instance.SpawnBomb();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
    }
}
