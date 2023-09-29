using Unity.Netcode;
using UnityEngine;

public class DisableCamera : NetworkBehaviour
{ 

    private void OnNetworkInstantiate()
    {
        if (!IsOwner)
        {
            GetComponent<Camera>().enabled = false;
        }
    }
}
