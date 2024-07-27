using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JokerTxt : MonoBehaviour
{
    private Text txt;
    private void Awake()
    {
        txt = GetComponent<Text>(); 
    }
    private void Update()
    {
        txt.text = SaveManager.instance.jokerCount.ToString();
    }
}
