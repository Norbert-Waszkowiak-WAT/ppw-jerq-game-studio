using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class weaponsHandler : NetworkBehaviour
{
    public GameObject[] weapons;

    public int currentWeaponIndex = 0;

    

    public Transform playerTransform;

    public void SelectWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length)
        {
            Debug.LogError("Index out of range");
            return;
        }

        weapons[currentWeaponIndex].SetActive(false);
        weapons[currentWeaponIndex].transform.parent = transform;
        weapons[currentWeaponIndex].transform.localPosition = Vector3.zero;
        weapons[currentWeaponIndex].transform.localRotation = Quaternion.identity;
        weapons[index].SetActive(true);
        weapons[index].transform.parent = playerTransform;
        weapons[index].transform.localPosition = weapons[index].GetComponent<GunStats>().thisWeapon.weaponPosition;
        weapons[index].transform.localRotation = weapons[index].GetComponent<GunStats>().thisWeapon.weaponRotation;

        
        playerTransform.GetComponent<shootingWithRaycasts>().gun = weapons[index];
        

        playerTransform.GetComponent<shootingWithRaycasts>().damage = weapons[index].GetComponent<GunStats>().thisWeapon.damage;
        playerTransform.GetComponent<shootingWithRaycasts>().fireRate = weapons[index].GetComponent<GunStats>().thisWeapon.fireRate;
        playerTransform.GetComponent<shootingWithRaycasts>().range = weapons[index].GetComponent<GunStats>().thisWeapon.range;
        playerTransform.GetComponent<shootingWithRaycasts>().headshotMultiplier = weapons[index].GetComponent<GunStats>().thisWeapon.headshotMultiplier;
        playerTransform.GetComponent<shootingWithRaycasts>().legshotMultiplier = weapons[index].GetComponent<GunStats>().thisWeapon.legshotMultiplier;

        playerTransform.GetComponent<shootingWithRaycasts>().WaponChanged();


        currentWeaponIndex = index;
    }


    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectWeapon(2);
        }
    }
}
