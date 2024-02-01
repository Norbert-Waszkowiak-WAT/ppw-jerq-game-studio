using Unity.Netcode;
using UnityEngine;

public class DisableCamera : NetworkBehaviour
{
    Camera thisCamera;

    private void Awake()
    {
        thisCamera = GetComponent<Camera>();
    }

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            thisCamera.enabled = false;
        }
        else
        {
            thisCamera.enabled = true;
        }
    } 
}
