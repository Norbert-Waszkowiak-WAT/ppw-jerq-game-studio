using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class weaponsHandlerSingleplayer : MonoBehaviour
{
    public GameObject[] weapons;

    public int currentWeaponIndex = 0;

    

    public Transform playerTransform;

    public void Awake()
    {
        SelectWeapon(0);
    }

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

        
        playerTransform.GetComponent<shootingWithRaycastsSingleplayer>().gun = weapons[index];
        

        playerTransform.GetComponent<shootingWithRaycastsSingleplayer>().damage = weapons[index].GetComponent<GunStats>().thisWeapon.damage;
        playerTransform.GetComponent<shootingWithRaycastsSingleplayer>().fireRate = weapons[index].GetComponent<GunStats>().thisWeapon.fireRate;
        playerTransform.GetComponent<shootingWithRaycastsSingleplayer>().range = weapons[index].GetComponent<GunStats>().thisWeapon.range;
        playerTransform.GetComponent<shootingWithRaycastsSingleplayer>().headshotMultiplier = weapons[index].GetComponent<GunStats>().thisWeapon.headshotMultiplier;
        playerTransform.GetComponent<shootingWithRaycastsSingleplayer>().legshotMultiplier = weapons[index].GetComponent<GunStats>().thisWeapon.legshotMultiplier;

        playerTransform.GetComponent<shootingWithRaycastsSingleplayer>().WaponChanged();


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
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectWeapon(2);
        }
    }
}
