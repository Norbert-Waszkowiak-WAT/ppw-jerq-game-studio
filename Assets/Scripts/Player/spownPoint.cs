using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spownPoint : MonoBehaviour
{
    public Vector3 spownPosition;

    public float heightTreshold = 0.65f;

    void Update()
    {
        if (transform.position.y < heightTreshold)
        {
            transform.position = spownPosition;
        }
    }
}
