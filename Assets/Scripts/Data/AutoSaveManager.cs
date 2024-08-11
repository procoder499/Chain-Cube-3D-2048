using UnityEngine;

public class AutoSaveManager : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        SaveManager.instance.newMaxScore = 0;
        SaveManager.instance.currentScore = 0;
        SaveManager.instance.isButtonPressed = false;
        SaveManager.instance.Save();
    }
}