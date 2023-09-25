using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class BlockNotMine : MonoBehaviour
{
    public List<MonoBehaviour> scriptsToBlock = new List<MonoBehaviour>();

    public GameObject playerCamera; //delete other players cams

    private PhotonView photonView;

    private void Start()
    {
        playerCamera = GetComponentInChildren<Camera>().gameObject;

        photonView = GetComponent<PhotonView>();

        if (!photonView.IsMine)
        {
            Destroy(playerCamera);
            foreach (MonoBehaviour script in scriptsToBlock)
            {
                script.enabled = false;
            }
        }
    }
}
