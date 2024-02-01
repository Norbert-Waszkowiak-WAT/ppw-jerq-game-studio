using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class weaponsLoadoutHandler : MonoBehaviour
{
    public int currentWeaponType = 0;
    public string[] weaponTypes;
    public GameObject[] weaponTypesSelectors;

    public TMP_Text weaponTypeText;

    public GameObject[] weaponPlaces;

    public void Next()
    {
        LogWriter.WriteLog("Next() called");
        currentWeaponType++;
        if (currentWeaponType >= weaponTypes.Length)
        {
            currentWeaponType = 0;
            weaponTypesSelectors[0].SetActive(true);
            weaponTypesSelectors[weaponTypesSelectors.Length - 1].SetActive(false);

            //weaponPlaces[0].SetActive(true);
            //weaponPlaces[weaponTypesSelectors.Length - 1].SetActive(false);

            if (weaponPlaces[0].transform.childCount == 0)
            {
                weaponPlaces[0].GetComponent<Image>().color = new Color(
                    weaponPlaces[0].GetComponent<Image>().color.r,
                    weaponPlaces[0].GetComponent<Image>().color.g,
                    weaponPlaces[0].GetComponent<Image>().color.b,
                    1f
                );
            }

            ActivateDisactivateRaycastOnWeapons(weaponPlaces[0], true);

            weaponPlaces[weaponTypesSelectors.Length - 1].GetComponent<Image>().color = new Color(
                weaponPlaces[weaponTypesSelectors.Length - 1].GetComponent<Image>().color.r,
                weaponPlaces[weaponTypesSelectors.Length - 1].GetComponent<Image>().color.g,
                weaponPlaces[weaponTypesSelectors.Length - 1].GetComponent<Image>().color.b,
                0f
                );

            ActivateDisactivateRaycastOnWeapons(weaponPlaces[0], false);

            weaponTypeText.text = weaponTypes[0];
            return;
        }
        weaponTypeText.text = weaponTypes[currentWeaponType];
        weaponTypesSelectors[currentWeaponType].SetActive(true);
        weaponTypesSelectors[currentWeaponType - 1].SetActive(false);

        //weaponPlaces[currentWeaponType].SetActive(true);
        //weaponPlaces[currentWeaponType - 1].SetActive(false);

        if (weaponPlaces[currentWeaponType].transform.childCount == 0)
        {
            weaponPlaces[currentWeaponType].GetComponent<Image>().color = new Color(
                weaponPlaces[currentWeaponType].GetComponent<Image>().color.r,
                weaponPlaces[currentWeaponType].GetComponent<Image>().color.g,
                weaponPlaces[currentWeaponType].GetComponent<Image>().color.b,
                1f
                );
        }

        ActivateDisactivateRaycastOnWeapons(weaponPlaces[currentWeaponType], true);

        weaponPlaces[currentWeaponType - 1].GetComponent<Image>().color = new Color(
                weaponPlaces[currentWeaponType - 1].GetComponent<Image>().color.r,
                weaponPlaces[currentWeaponType - 1].GetComponent<Image>().color.g,
                weaponPlaces[currentWeaponType - 1].GetComponent<Image>().color.b,
                0f
                );

        ActivateDisactivateRaycastOnWeapons(weaponPlaces[currentWeaponType], false);

    }   
    
    public void Previous()
    {
        LogWriter.WriteLog("Previous() called");
        currentWeaponType--;
        if (currentWeaponType < 0)
        {
            currentWeaponType = weaponTypes.Length - 1;
            weaponTypesSelectors[0].SetActive(false);
            weaponTypesSelectors[currentWeaponType].SetActive(true);

            //weaponPlaces[0].SetActive(false);
            //weaponPlaces[currentWeaponType].SetActive(true);

            weaponPlaces[0].GetComponent<Image>().color = new Color(
                weaponPlaces[0].GetComponent<Image>().color.r,
                weaponPlaces[0].GetComponent<Image>().color.g,
                weaponPlaces[0].GetComponent<Image>().color.b,
                0f
                );

            ActivateDisactivateRaycastOnWeapons(weaponPlaces[0], false);

            if (weaponPlaces[currentWeaponType].transform.childCount == 0 )
            {
                weaponPlaces[currentWeaponType].GetComponent<Image>().color = new Color(
                    weaponPlaces[currentWeaponType].GetComponent<Image>().color.r,
                    weaponPlaces[currentWeaponType].GetComponent<Image>().color.g,
                    weaponPlaces[currentWeaponType].GetComponent<Image>().color.b,
                    1f
                    );
            }

            ActivateDisactivateRaycastOnWeapons(weaponPlaces[currentWeaponType], true);

            weaponTypeText.text = weaponTypes[currentWeaponType];
            return;
        }
        weaponTypeText.text = weaponTypes[currentWeaponType];
        weaponTypesSelectors[currentWeaponType].SetActive(true);
        weaponTypesSelectors[currentWeaponType + 1].SetActive(false);

        //weaponPlaces[currentWeaponType].SetActive(true);
        //weaponPlaces[currentWeaponType + 1].SetActive(false);

        if (weaponPlaces[currentWeaponType].transform.childCount == 0)
        {
            weaponPlaces[currentWeaponType].GetComponent<Image>().color = new Color(
                weaponPlaces[currentWeaponType].GetComponent<Image>().color.r,
                weaponPlaces[currentWeaponType].GetComponent<Image>().color.g,
                weaponPlaces[currentWeaponType].GetComponent<Image>().color.b,
                1f
                );
        }

        ActivateDisactivateRaycastOnWeapons(weaponPlaces[currentWeaponType], true);

        weaponPlaces[currentWeaponType + 1].GetComponent<Image>().color = new Color(
                weaponPlaces[currentWeaponType + 1].GetComponent<Image>().color.r,
                weaponPlaces[currentWeaponType + 1].GetComponent<Image>().color.g,
                weaponPlaces[currentWeaponType + 1].GetComponent<Image>().color.b,
                0f
                );

        ActivateDisactivateRaycastOnWeapons(weaponPlaces[currentWeaponType + 1], false);
    }

    private void ActivateDisactivateRaycastOnWeapons(GameObject parent, bool activate)
    {
        GameObject[] weapons = new GameObject[parent.transform.childCount];

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            weapons[i] = parent.transform.GetChild(i).gameObject;
        }

        foreach (GameObject weapon in weapons)
        {
            moveableWeapon moveableWeapon = weapon.GetComponent<moveableWeapon>();
            Image image = weapon.GetComponent<Image>();
            if (moveableWeapon != null && image  != null)
            {
                image.raycastTarget = activate;
            }
        }
    }
}
