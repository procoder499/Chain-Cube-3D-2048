using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    int checkNumber = 128;
    [SerializeField] private GameObject CollapsedWindow;
    [SerializeField] private GameObject LoseWindow;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if(SaveManager.instance.newMaxScore >= checkNumber)
        {
            Audio.instance.SmallSuccess();
            CollapsedWindow.SetActive(true);
            CollapsedSkipText.instance.Skip();
            checkNumber *= 2;
            UIcollapsed.instance.ScaleChange();
        }
    }

    public void LoseWindowEvent()
    {
        LoseWindow.SetActive(true);
        LoseMenu.instance.StartDecreaseCircleTimer();
    }
    public void CubeCollapsedEvent()
    {
        int RandomValue = Random.Range(0, 2);
        if(RandomValue == 0)
        {
            SaveManager.instance.jokerCount++;

        }
        else
        {
            SaveManager.instance.bombCount++;

        }
        SaveManager.instance.Save();
    }
    public void CubeCollapsedEvent_1()
    {
        int RandomValue = Random.Range(0, 2);
        if (RandomValue == 0)
        {
            SaveManager.instance.jokerCount += 2;

        }
        else
        {
            SaveManager.instance.bombCount += 2;

        }
        SaveManager.instance.Save();
    }
}
