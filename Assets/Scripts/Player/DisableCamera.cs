using Unity.Netcode;
using UnityEngine;

public class DisableCamera : NetworkBehaviour
{ 
    /*void Awake()
    {
        if (!IsOwner)
        {
            GetComponent<Camera>().enabled = false;
        }
    }*/

    private void OnNetworkInstantiate()
    {
        if (!IsOwner)
        {
            GetComponent<Camera>().enabled = false;
        }
    }
}
