using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class weaponsHandler : NetworkBehaviour
{
    public Weapon[] weapons;

    public GameObject[] weaponsGameObjects;

    public int currentWeaponIndex = 0;

    

    public Transform playerTransform;

    public void SelectWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length)
        {
            Debug.LogError("Index out of range");
            return;
        }

        weaponsGameObjects[currentWeaponIndex].SetActive(false);
        weaponsGameObjects[currentWeaponIndex].transform.parent = transform;
        weaponsGameObjects[currentWeaponIndex].transform.localPosition = Vector3.zero;
        weaponsGameObjects[currentWeaponIndex].transform.localRotation = Quaternion.identity;
        weaponsGameObjects[index].SetActive(true);
        weaponsGameObjects[index].transform.parent = playerTransform;
        weaponsGameObjects[index].transform.localPosition = weapons[index].weaponPosition;
        weaponsGameObjects[index].transform.localRotation = weapons[index].weaponRotation;

        playerTransform.GetComponent<shootingWithRaycasts>().gun = weaponsGameObjects[index];

        playerTransform.GetComponent<shootingWithRaycasts>().damage = weapons[index].damage;
        playerTransform.GetComponent<shootingWithRaycasts>().fireRate = weapons[index].fireRate;
        playerTransform.GetComponent<shootingWithRaycasts>().range = weapons[index].range;

        currentWeaponIndex = index;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectWeapon(1);
        }
    }
    public override void OnNetworkSpawn()
    {
        FindAndSetPlayer();
        SelectWeapon(currentWeaponIndex);
    }

    private void FindAndSetPlayer()
    {
        // Find the player GameObject with the "Player" tag.
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in players)
        {
            NetworkObject networkObject = p.GetComponent<NetworkObject>();

            if (networkObject != null && networkObject.OwnerClientId == OwnerClientId)
            {
                // Set the playerTransform if it's owned by the local client.
                playerTransform = p.transform;
                break;
            }
        }
    }
}
