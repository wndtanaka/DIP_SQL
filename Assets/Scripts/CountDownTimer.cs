using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// countdown timer that takes user to the login screen
public class CountDownTimer : MonoBehaviour
{
    #region Variables
// make them static so easily access by another script, so when it reaches zero, it will direct to another scene.
    public static float countDownTimer = 5;
    public static bool canStartCount = false;
    public Text timeText;
    #endregion
    
    void Update()
    {
        // when canStartCount is true
        if (canStartCount)
        {
            // set timeText
            timeText.text = "Password changed. Taking you back to login screen in " + countDownTimer.ToString("F0") + " second(s)";
            // if the countDownTimer less or equal to zero
            if (countDownTimer <= 0)
            {
                // disable timeText
                timeText.enabled = false;
                // return
                return;
            }
            // if countDownTimer is greater than zero
            if (countDownTimer > 0)
            {
                // enable timeText
                timeText.enabled = true;
            }
            // countDownTimer decrement by deltatime
            countDownTimer -= Time.deltaTime;
        }
    }
}
