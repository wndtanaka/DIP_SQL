using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// countdown timer fore resending another email
public class Timer : MonoBehaviour
{
    public static float resendTimer = 60;
    public Button resendButton;
    public Text timeText;

    void Update()
    {
        timeText.text = "Please wait " + resendTimer.ToString("F0") + " second(s) before sending another code";
        if (resendTimer <= 0)
        {
            resendButton.interactable = true;
            resendButton.GetComponent<EventTrigger>().enabled = true;
            timeText.enabled = false;

            return;
        }
        if (resendTimer > 0)
        {
            resendButton.interactable = false;
            if (resendButton.interactable == false)
            {
                resendButton.GetComponent<EventTrigger>().enabled = false;
            }
            timeText.enabled = true;
        }
        resendTimer -= Time.deltaTime;
    }
}
