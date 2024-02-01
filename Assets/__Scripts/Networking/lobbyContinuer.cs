using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class lobbyContinuer : NetworkBehaviour
{
    public GameObject dataTransferObject;
    public testRelay testRelayInstance;

    private List<int> securityInts = new List<int>() { 0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20};
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
        dataTransferGot.lobbyQuit = true;
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
            weaponSelecter weaponSelecterGot = player.GetComponent<weaponSelecter>();
            if (weaponSelecterGot != null)
            {
                if (dataTransferObject.GetComponent<dataTransfer>() != null)
                {
                    weaponSelecterGot.ChooseWeapons(dataTransferObject.GetComponent<dataTransfer>().selectedWeapons);
                }
                else
                {
                    weaponSelecterGot.ChooseWeapons(securityInts);
                }
                
            }
            
        }
    }
}
