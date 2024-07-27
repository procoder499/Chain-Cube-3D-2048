using UnityEngine;

public class AutoSaveManager : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        SaveManager.instance.currentScore = 0;
        SaveManager.instance.Save();
    }
}