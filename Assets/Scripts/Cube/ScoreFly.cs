using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreFly : MonoBehaviour
{
    public static ScoreFly Instance;
    private Text txt;
    public int score;
    private void Start()
    {
        Instance = this;
        txt = GetComponent<Text>();
    }

    private void Update()
    {
        txt.text = "+ " + score;
    }
}
