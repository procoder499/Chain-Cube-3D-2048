using UnityEngine;
using UnityEngine.EventSystems;

public class JokerPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        SaveManager.instance.isButtonPressed = true;
        if (SaveManager.instance.jokerCount >= 1)
        {
            SaveManager.instance.jokerCount--;
            SaveManager.instance.Save();
            ControlMovement.instance.mainCube.gameObject.SetActive(false);
            ControlMovement.instance.mainCube = CubeSpawnController.Instance.SpawnJoker();
            ControlMovement.instance.SpawnMainCube();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SaveManager.instance.isButtonPressed = false;
    }
}
