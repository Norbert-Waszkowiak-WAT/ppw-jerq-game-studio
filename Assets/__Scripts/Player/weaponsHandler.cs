using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class weaponsHandler : NetworkBehaviour
{
    public GameObject playingPlayer;

    public List<GameObject> weapons;

    public int currentWeaponIndex = 0;

    public shootingWithRaycasts shootingWithRaycastsInstance;

    public Transform playerTransform;

    public GameObject playerRHand;

    public NetworkObject localNetworkobject;

    public void SelectWeapon(int index)
    {
        LogWriter.WriteLog("SelectWeapon called: " + index);
        if (index < 0 || index >= weapons.Count)
        {
            LogWriter.WriteLog("Index out of range, not enough weapons");
            Debug.LogError("Index out of range");
            return;
        }

        weapons[currentWeaponIndex].SetActive(false);
        weapons[currentWeaponIndex].transform.parent = transform;
        weapons[currentWeaponIndex].transform.localPosition = Vector3.zero;
        weapons[currentWeaponIndex].transform.localRotation = Quaternion.identity;
        weapons[index].SetActive(true);
        weapons[index].transform.parent = playerRHand.transform;
        weapons[index].transform.localPosition = weapons[index].GetComponent<GunStats>().thisWeapon.weaponPosition;
        weapons[index].transform.localRotation = Quaternion.Euler(weapons[index].GetComponent<GunStats>().thisWeapon.weaponRotation);

        
        playerTransform.GetComponent<shootingWithRaycasts>().gun = weapons[index];
        

        playerTransform.GetComponent<shootingWithRaycasts>().damage = weapons[index].GetComponent<GunStats>().thisWeapon.damage;
        playerTransform.GetComponent<shootingWithRaycasts>().fireRate = weapons[index].GetComponent<GunStats>().thisWeapon.fireRate;
        playerTransform.GetComponent<shootingWithRaycasts>().range = weapons[index].GetComponent<GunStats>().thisWeapon.range;
        playerTransform.GetComponent<shootingWithRaycasts>().headshotMultiplier = weapons[index].GetComponent<GunStats>().thisWeapon.headshotMultiplier;
        playerTransform.GetComponent<shootingWithRaycasts>().legshotMultiplier = weapons[index].GetComponent<GunStats>().thisWeapon.legshotMultiplier;

        playerTransform.GetComponent<shootingWithRaycasts>().WaponChanged();


        currentWeaponIndex = index;
        SendUpdateToOtherPlayers();
    }

    void SendUpdateToOtherPlayers()
    {
        LogWriter.WriteLog("SendUpdateToOtherPlayers called");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("len of players: " +  players.Length);
        LogWriter.WriteLog("len of players: " + players.Length);
        foreach (GameObject player in players)
        {
            if (player.GetComponent<NetworkObject>() != null)
            {
                if (player.GetComponent<NetworkObject>().NetworkObjectId != localNetworkobject.NetworkObjectId)
                {

                    weaponsHandler weaponHandler = player.GetComponent<weaponsHandlerHolder>().weaponsHandlerInstance;
                    if (NetworkManager.Singleton.IsServer)
                    {
                        //clientRpc
                        if (weaponHandler != null)
                        {
                            Debug.LogError("sending client rpc to: " + player.name);
                            LogWriter.WriteLog("sending client rpc to: " + player.name);    
                            weaponHandler.SelectWeaponForSenderClientRpc(currentWeaponIndex, playingPlayer.GetComponent<NetworkObject>().NetworkObjectId);
                        }
                    }
                    else
                    {
                        if (weaponHandler != null)
                        {
                            Debug.LogError("sending server rpc to: " + player.name);
                            LogWriter.WriteLog("sending server rpc to: " + player.name);
                            weaponHandler.SelectWeaponForSenderServerRpc(currentWeaponIndex, playingPlayer.GetComponent<NetworkObject>().NetworkObjectId);
                        }
                    }
                }
            }
        }
    }    

    [ServerRpc(RequireOwnership = false)]
    public void SelectWeaponForSenderServerRpc(int index, ulong Id)
    {
        Debug.LogError("SelectWeaponForSenderServerRpc id: " + Id.ToString() + " index: " + index);
        LogWriter.WriteLog("SelectWeaponForSenderServerRpc id: " + Id.ToString() + " index: " + index);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<NetworkObject>().NetworkObjectId == Id)
            {
                Debug.Log("Done");
                player.GetComponent<weaponsHandlerHolder>().weaponsHandlerInstance.SelectWeapon(index);
            }
        }
        //SelectWeapon(index);
    }

    [ClientRpc]
    public void SelectWeaponForSenderClientRpc(int index, ulong Id)
    {
        Debug.LogError("SelectWeaponForSenderClientRpc id: " + Id.ToString() + " index: " + index);
        LogWriter.WriteLog("SelectWeaponForSenderClientRpc id: " + Id.ToString() + " index: " + index);
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.GetComponent<NetworkObject>().NetworkObjectId == Id)
            {
                Debug.Log("Done");
                player.GetComponent<weaponsHandlerHolder>().weaponsHandlerInstance.SelectWeapon(index);
            }
        }
        //SelectWeapon(index);
    }


    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && weapons.Count >= 1)
        {
            SelectWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count >= 2)
        {
            SelectWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Count >= 3)
        {
            SelectWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && weapons.Count >= 4)
        {
            SelectWeapon(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && weapons.Count >= 5)
        {
            SelectWeapon(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && weapons.Count >= 6)
        {
            SelectWeapon(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) && weapons.Count >= 7)
        {
            SelectWeapon(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) && weapons.Count >= 8)
        {
            SelectWeapon(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) && weapons.Count >= 9)
        {
            SelectWeapon(8);
        }


    }

    private void Awake()
    {
        SelectWeapon(0);
    }
}
