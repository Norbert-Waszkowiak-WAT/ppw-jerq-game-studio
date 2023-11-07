using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class lobbyContinuer : NetworkBehaviour
{
    public GameObject dataTransferObject;
    public testRelay testRelayInstance;

    private void Start()
    {
        dataTransferObject = GameObject.Find("Data Transfer");
        dataTransfer dataTransferGot = dataTransferObject.GetComponent<dataTransfer>();
        if (dataTransferGot.isHost) {
            NetworkManager.Singleton.StartHost();
        }
        else
        {
            testRelayInstance.JoinRelay(dataTransferGot.relayCode);
        }
    }

    public override void OnNetworkSpawn()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            addMaterialsToPlayerInMenu addMaterialsToPlayerInMenuGot = player.GetComponent<addMaterialsToPlayerInMenu>();
            if (addMaterialsToPlayerInMenuGot != null)
            {
                addMaterialsToPlayerInMenuGot.mat1 = dataTransferObject.GetComponent<dataTransfer>().materials[0];
                addMaterialsToPlayerInMenuGot.mat2 = dataTransferObject.GetComponent<dataTransfer>().materials[1];
                addMaterialsToPlayerInMenuGot.mat3 = dataTransferObject.GetComponent<dataTransfer>().materials[2];
                addMaterialsToPlayerInMenuGot.Paint();
            }
        }
    }
}
