using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit : MonoBehaviour
{
    public menuButtons menuButtons;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
        {
            LogWriter.WriteLog("Exit button pressed");
            menuButtons.Quit();
        }
    }
}
