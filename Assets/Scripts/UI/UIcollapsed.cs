using System.Collections;
using UnityEngine;

public class UIcollapsed : MonoBehaviour
{
    public static UIcollapsed instance;
    public RectTransform panelRectTransform;
    public float animationDuration = 0.7f;

    void Awake()
    {
        instance = this;

    }
    public void ScaleChange()
    {
        if (panelRectTransform != null)
        {
            panelRectTransform.localScale = Vector3.zero;
        }
        else
        {
            Debug.LogError("Panel RectTransform is not assigned.");
        }
        StartCoroutine(ScaleUp());
    }
    IEnumerator ScaleUp()
    {
        if (panelRectTransform == null)
        {
            yield break;
        }

        float timeElapsed = 0f;

        while (timeElapsed < animationDuration)
        {
            timeElapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(timeElapsed / animationDuration);
            panelRectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, progress);
            yield return null;
        }

        panelRectTransform.localScale = Vector3.one; // Đảm bảo scale cuối cùng bằng 1
    }
}