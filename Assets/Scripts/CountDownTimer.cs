using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// countdown timer that takes user to the login screen
public class CountDownTimer : MonoBehaviour
{
    // make them static so easily access by another script
    public static float countDownTimer = 5;
    public static bool canStartCount = false;
    public Text timeText;

    void Update()
    {
        if (canStartCount)
        {
            timeText.text = "Password changed. Taking you back to login screen in " + countDownTimer.ToString("F0") + " second(s)";
            if (countDownTimer <= 0)
            {
                timeText.enabled = false;

                return;
            }
            if (countDownTimer > 0)
            {
                timeText.enabled = true;
            }
            countDownTimer -= Time.deltaTime;
        }
    }
}
