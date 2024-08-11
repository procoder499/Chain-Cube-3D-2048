using UnityEngine;
using UnityEngine.EventSystems;

public class BombPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        SaveManager.instance.isButtonPressed = true;
        if (SaveManager.instance.bombCount >= 1)
        {
            SaveManager.instance.bombCount--;
            SaveManager.instance.Save();
            ControlMovement.instance.mainCube.gameObject.SetActive(false);
            ControlMovement.instance.mainCube = CubeSpawnController.Instance.SpawnBomb();
            ControlMovement.instance.SpawnMainCube();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SaveManager.instance.isButtonPressed = false;
    }
}