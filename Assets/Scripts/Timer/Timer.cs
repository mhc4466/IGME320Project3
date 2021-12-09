using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Timer : MonoBehaviour
{
    TMP_Text Score;
    float time = 0;
    private void Start()
    {
        time = 0;
    }


    private void Update()
    {
        time += Time.deltaTime;
        Score.text = time.ToString();
    }
}
