using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CollapsedSkipText : MonoBehaviour
{
    public static CollapsedSkipText instance;
    public Button skipButton; 

    void Awake()
    {
        instance = this;
    }
    public void Skip()
    {
        skipButton.gameObject.SetActive(false);
        StartCoroutine(SkipText());
        SaveManager.instance.isButtonPressed = true;
    }
    IEnumerator SkipText()
    {
        yield return new WaitForSeconds(2);
        skipButton.gameObject.SetActive(true); 
        skipButton.interactable = true; 
    }
}