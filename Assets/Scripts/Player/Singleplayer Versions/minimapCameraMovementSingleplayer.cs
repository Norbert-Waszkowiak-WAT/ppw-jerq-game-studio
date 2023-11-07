using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MinimapCameraMovementSingleplayer : MonoBehaviour
{
    public Transform playerTransform;


    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            Vector3 newPosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
            transform.position = newPosition;

            transform.rotation = Quaternion.Euler(90f, playerTransform.eulerAngles.y, 0f);
        }
    }
}
