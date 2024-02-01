using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomizeName : MonoBehaviour
{
    private void Awake()
    {
        gameObject.name = gameObject.name + Random.Range(0, 10000);
        LogWriter.WriteLog("Randomize name" + gameObject.name);
    }
}
