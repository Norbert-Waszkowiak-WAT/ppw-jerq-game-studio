using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class focusHeadTargets : MonoBehaviour
{
    public GameObject[] headTargets;

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject headTarget in headTargets)
        {
            headTarget.transform.position = transform.position;
        }
    }
}
