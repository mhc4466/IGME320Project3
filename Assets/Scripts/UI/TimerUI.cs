using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TimerUI : MonoBehaviour
{
    [SerializeField]TMP_Text text;
    [SerializeField] TimeHolder time;
    void Start()
    {
        float timeTotal = time.TimeForGame;
        int minutes = Mathf.FloorToInt(timeTotal / 60F);
        int seconds = Mathf.FloorToInt(timeTotal % 60F);
        int milliseconds = Mathf.FloorToInt((timeTotal * 100F) % 100F);
        text.text = minutes.ToString("00") + ":" + seconds.ToString("00") + ":" + milliseconds.ToString("00");
    }
}
