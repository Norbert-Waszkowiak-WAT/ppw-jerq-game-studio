using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataTransferQuitExtractor : MonoBehaviour
{
    public GameObject dataTransferObject;
    public lobbyListHandler lobbyListHandlerInstance;
    private void Start()
    {
        dataTransferObject = GameObject.Find("Data Transfer");
        dataTransfer dataTransferGot = dataTransferObject.GetComponent<dataTransfer>();
        lobbyListHandlerInstance.isLobbyQuit = dataTransferGot.lobbyQuit;
    }
}
