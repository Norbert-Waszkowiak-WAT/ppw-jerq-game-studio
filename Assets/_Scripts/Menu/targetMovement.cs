using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetMovement : MonoBehaviour
{
    public Vector3 positionDiffrence;
    void Update()
    {
        
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + positionDiffrence;
    }
}
