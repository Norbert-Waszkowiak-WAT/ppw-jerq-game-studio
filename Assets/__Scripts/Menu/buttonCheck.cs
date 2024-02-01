using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonCheck : MonoBehaviour
{
    public void ButtonCheck()
    {
        LogWriter.WriteLog("Button pressed");
        Debug.LogError("Button pressed");
    }
}
