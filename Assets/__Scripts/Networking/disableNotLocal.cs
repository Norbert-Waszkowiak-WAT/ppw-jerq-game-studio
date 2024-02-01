using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class disableNotLocal : NetworkBehaviour
{
    public List<Behaviour> scriptsToDisable;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            foreach (Behaviour script in scriptsToDisable)
            {
                script.enabled = false;
            }
        }
    }
}
