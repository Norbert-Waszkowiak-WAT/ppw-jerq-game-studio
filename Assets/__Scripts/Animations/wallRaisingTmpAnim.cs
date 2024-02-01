using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRaisingTmpAnim : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 startScale;

    public float PosY;
    public float ScaleY;

    private BoxCollider boxCollider;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        startPosition = transform.position;
        startScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
    }

    private void Update()
    {
        transform.position = startPosition + new Vector3(0, PosY, 0);
        transform.localScale = startScale + new Vector3(0, ScaleY, 0);
        boxCollider.size = new Vector3(1, 1, 1);
        if (ScaleY == 1.5f)
        {
            enabled = false;
        }
    }

}
