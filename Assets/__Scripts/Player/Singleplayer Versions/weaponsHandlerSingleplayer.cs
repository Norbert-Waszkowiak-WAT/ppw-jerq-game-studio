using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class weaponsHandlerSingleplayer : MonoBehaviour
{
    public GameObject playingPlayer;

    public GameObject[] weapons;

    public int currentWeaponIndex = 0;

    public Transform playerTransform;

    public GameObject playerRHand;

    public Vector3 scaleUppener = Vector3.one;

    public bool enableWeaponSwitching = true;

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
        //weapons[currentWeaponIndex].transform.localScale = scaleUppener;
        weapons[index].SetActive(true);
        weapons[index].transform.parent = playerRHand.transform;
        weapons[index].transform.localPosition = weapons[index].GetComponent<GunStats>().thisWeapon.weaponPosition;
        weapons[index].transform.localRotation = Quaternion.Euler(weapons[index].GetComponent<GunStats>().thisWeapon.weaponRotation);
        currentWeaponIndex = index;
        shootingWithRaycastsSingleplayer shootingWithRaycastsSingleplayer = playerTransform.GetComponent<shootingWithRaycastsSingleplayer>();
        if (shootingWithRaycastsSingleplayer == null) return;
        shootingWithRaycastsSingleplayer.gun = weapons[index];


        shootingWithRaycastsSingleplayer.damage = weapons[index].GetComponent<GunStats>().thisWeapon.damage;
        shootingWithRaycastsSingleplayer.fireRate = weapons[index].GetComponent<GunStats>().thisWeapon.fireRate;
        shootingWithRaycastsSingleplayer.range = weapons[index].GetComponent<GunStats>().thisWeapon.range;
        shootingWithRaycastsSingleplayer.headshotMultiplier = weapons[index].GetComponent<GunStats>().thisWeapon.headshotMultiplier;
        shootingWithRaycastsSingleplayer.legshotMultiplier = weapons[index].GetComponent<GunStats>().thisWeapon.legshotMultiplier;

        shootingWithRaycastsSingleplayer.WaponChanged();


        
        //SendUpdateToOtherPlayers();
    }
    private void Update()
    {
        if (!enableWeaponSwitching) return;
        if (Input.GetKeyDown(KeyCode.Alpha1) && weapons.Length >= 1)
        {
            SelectWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Length >= 2)
        {
            SelectWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Length >= 3)
        {
            SelectWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && weapons.Length >= 4)
        {
            SelectWeapon(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && weapons.Length >= 5)
        {
            SelectWeapon(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && weapons.Length >= 6)
        {
            SelectWeapon(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) && weapons.Length >= 7)
        {
            SelectWeapon(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) && weapons.Length >= 8)
        {
            SelectWeapon(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) && weapons.Length >= 9)
        {
            SelectWeapon(8);
        }


    }

    private void Awake()
    {
        SelectWeapon(0);
    }

    public void SelectWeaponById(int id)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].GetComponent<GunStats>().thisWeapon.weaponIndex == id)
            {
                SelectWeapon(i);
                return;
            }
        }
    }
}
