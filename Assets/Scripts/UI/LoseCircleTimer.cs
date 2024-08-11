using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoseMenu : MonoBehaviour
{
    [SerializeField] private Image circleTimer;
    public static LoseMenu instance;

    private void Awake()
    {
        // Đảm bảo chỉ có một instance của LoseMenu
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        //StartDecreaseCircleTimer(); 
    }

    // Phương thức để khởi động việc giảm fillAmount của circleTimer về 0 trong vòng 8 giây
    public void StartDecreaseCircleTimer()
    {
        if (circleTimer != null)
        {
            StartCoroutine(DecreaseCircleTimer(8f)); // 8 giây để giảm fillAmount về 0
        }
        else
        {
            Debug.LogError("CircleTimer is not assigned.");
        }
    }

    private IEnumerator DecreaseCircleTimer(float duration)
    {
        float elapsedTime = 0f;
        float initialFillAmount = circleTimer.fillAmount;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float fillValue = Mathf.Lerp(initialFillAmount, 0f, elapsedTime / duration);
            circleTimer.fillAmount = fillValue;
            yield return null;
        }

        circleTimer.fillAmount = 0f; // Đảm bảo fillAmount đạt giá trị 0 khi hoàn tất
    }
}