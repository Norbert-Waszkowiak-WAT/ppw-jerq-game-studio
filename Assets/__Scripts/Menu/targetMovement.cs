using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetMovement : MonoBehaviour
{
    public Vector3 positionDiffrence;
    public Vector3 basePosition = Vector3.zero;
    public bool allowHorizontalMovement = true;
    public bool allowVerticalMovement = true;
    public bool allowDepthMovement = true;

    private float xPosition;
    private float yPosition;
    private float zPosition;

    private void Start()
    {
        transform.position = basePosition;
    }
    void Update()
    {
        xPosition = allowHorizontalMovement ? Camera.main.ScreenToWorldPoint(Input.mousePosition).x : basePosition.x;
        yPosition = allowVerticalMovement ? Camera.main.ScreenToWorldPoint(Input.mousePosition).y : basePosition.y;
        zPosition = allowDepthMovement ? Camera.main.ScreenToWorldPoint(Input.mousePosition).z : basePosition.z;
        transform.position = new Vector3(xPosition, yPosition, zPosition) + positionDiffrence;
    }
}

//linijka kodu, Artur Gartur 
