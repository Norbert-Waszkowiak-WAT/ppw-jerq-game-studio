using Unity.Netcode;
using UnityEngine;

public class DisableCamera : NetworkBehaviour
{ 

    private void Update()
    {
        if (!IsOwner)
        {
            GetComponent<Camera>().enabled = false;
        }
    }
}
