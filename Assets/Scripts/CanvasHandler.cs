using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasHandler : MonoBehaviour
{
    #region Variables
private CanvasScaler scaler;
    #endregion
    
    // Use this for initialization
    void Start()
    {
        // get CanvasScaler component for scaler
        scaler = GetComponent<CanvasScaler>();
        // set scaler uiScaleMode to ScaleWithScreenSize
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
