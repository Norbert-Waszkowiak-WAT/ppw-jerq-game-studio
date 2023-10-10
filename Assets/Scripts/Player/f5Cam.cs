using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class f5Cam : MonoBehaviour
{
    public KeyCode changeKey  = KeyCode.F5;

    public Camera fpCam;
    public Camera tpCam;

    public bool isFp = true;

    void Update()
    {
        if (Input.GetKeyDown(changeKey))
        {
            if (isFp)
            {
                fpCam.gameObject.SetActive(false);
                tpCam.gameObject.SetActive(true);
                Camera.SetupCurrent(tpCam);
                isFp = false;
            }
            else
            {
                fpCam.gameObject.SetActive(true);
                tpCam.gameObject.SetActive(false);
                Camera.SetupCurrent(fpCam);
                isFp = true;
            }
        }
    }
}
