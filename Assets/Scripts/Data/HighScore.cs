using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HighScore : MonoBehaviour
{
    private Text txt;

    private void Awake()
    {
        txt = GetComponent<Text>();
    }
    void Update()
    {
        txt.text = "Tốt nhất: " + SaveManager.instance.highScore ;
    }
}
