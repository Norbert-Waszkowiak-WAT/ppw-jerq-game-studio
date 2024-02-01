using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MinimapCameraMovement : NetworkBehaviour
{
    private Transform playerTransform; // Cache the player's transform instead of the whole GameObject.

    private void Start()
    {
        // Initialize the playerTransform in Start to avoid unnecessary FindGameObjectsWithTag calls.
        FindAndSetPlayer();
    }

    private void LateUpdate()
    {
        if (playerTransform != null)
        {
            // Use playerTransform instead of player GameObject for performance.
            Vector3 newPosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
            transform.position = newPosition;

            // You can directly access the player's rotation.
            transform.rotation = Quaternion.Euler(90f, playerTransform.eulerAngles.y, 0f);
        }
    }

    public override void OnNetworkSpawn()
    {
        FindAndSetPlayer();
    }

    private void FindAndSetPlayer()
    {
        // Find the player GameObject with the "Player" tag.
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in players)
        {
            NetworkObject networkObject = p.GetComponent<NetworkObject>();

            if (networkObject != null && networkObject.OwnerClientId == OwnerClientId)
            {
                // Set the playerTransform if it's owned by the local client.
                playerTransform = p.transform;
                break;
            }
        }
    }
}
