using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// countdown timer fore resending another email
public class Timer : MonoBehaviour
{
    #region MyRegion
    public static float resendTimer = 60;
    public Button resendButton;
    public Text timeText;
    #endregion

    void Update()
    {
        // set timeText text accordingly
        timeText.text = "Please wait " + resendTimer.ToString("F0") + " second(s) before sending another code";
        // if resendTimer is zero or lower then, resendButton will be enabled to used.
        if (resendTimer <= 0)
        {
            // set resendButton interactable
            resendButton.interactable = true;
            // disable resendButton EventTrigger component
            resendButton.GetComponent<EventTrigger>().enabled = true;
            // disable timeText
            timeText.enabled = false;
            // return
            return;
        }
        // if resendTimer is zero or lower then, resendButton will be unabled to used.
        if (resendTimer > 0)
        {
            // resendButton not interactable
            resendButton.interactable = false;
            // if resendButton.interactable is false
            if (resendButton.interactable == false)
            {
                // disable resendButton EventTrigger component
                resendButton.GetComponent<EventTrigger>().enabled = false;
            }
            // enabled timeText
            timeText.enabled = true;
        }
        // counting down the resendTimer
        resendTimer -= Time.deltaTime;
    }
}
