using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public static bool isActive;
    public TextMeshProUGUI timerText;
    float start = 60f;
    public GameObject timerDisplay;

    private void Start()
    {
        timerDisplay.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            timerDisplay.SetActive(true);
            if (start > 0) start -= Time.deltaTime;
            else start = 0;

            double time = Math.Round(start, 2);
            timerText.text = time.ToString("0.00");
        }
    }

    static void StartTimer()
    {
        if (!isActive) isActive = true;
    }
}
