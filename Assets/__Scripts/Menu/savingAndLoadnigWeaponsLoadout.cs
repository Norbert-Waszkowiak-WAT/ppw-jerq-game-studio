using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class savingAndLoadnigWeaponsLoadout : MonoBehaviour
{
    public GameObject dataTransfer;

    public GameObject longWeaponPlace;
    public GameObject shortWeaponPlace;
    public GameObject meleeWeaponPlace;

    public GameObject[] longWeapons;
    public GameObject[] shortWeapons;
    public GameObject[] meleeWeapons;

    public GameObject weaponsHandlerOfPlayerInMainMenu;

    void Start()
    {
        weaponsLoadout loadout = new weaponsLoadout();
        LogWriter.WriteLog("Starting loading weapons loadout");
        if (File.Exists(Application.persistentDataPath + "/weaponsLoadout.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/weaponsLoadout.json");
            loadout = JsonUtility.FromJson<weaponsLoadout>(json);
            LogWriter.WriteLog("weapons loadout found");
        }
        else
        {
            loadout = new weaponsLoadout();
            loadout.longSlotWeapon = 3;
            loadout.shortSlotWeapon = 10;
            loadout.meleeSlotWeapon = 13;

            string json = JsonUtility.ToJson(loadout);
            File.WriteAllText(Application.persistentDataPath + "/weaponsLoadout.json", json);
            LogWriter.WriteLog("weapons loadout not found, creating new one");
        }
        dataTransfer.GetComponent<dataTransfer>().selectedWeapons = new List<int>();
        dataTransfer.GetComponent<dataTransfer>().selectedWeapons.Add(loadout.longSlotWeapon);
        dataTransfer.GetComponent<dataTransfer>().selectedWeapons.Add(loadout.shortSlotWeapon);
        dataTransfer.GetComponent<dataTransfer>().selectedWeapons.Add(loadout.meleeSlotWeapon);
        foreach(GameObject weapon in longWeapons)
        {
            GunStats gunStats = weapon.GetComponent<GunStats>();
            Weapon weaponStats = gunStats.thisWeapon;
            Debug.Log("weapon index: " + weaponStats.weaponIndex);
            if (weaponStats.weaponIndex == loadout.longSlotWeapon)
            {
                Debug.Log("long weapon found");
                LogWriter.WriteLog("long weapon found: " + weaponStats.weaponIndex);
                weapon.transform.SetParent(longWeaponPlace.transform);
                weapon.transform.position = longWeaponPlace.transform.position;
                weapon.transform.rotation = longWeaponPlace.transform.rotation;
                longWeaponPlace.GetComponent<Image>().color = new Color(
                    longWeaponPlace.GetComponent<Image>().color.r,
                    longWeaponPlace.GetComponent<Image>().color.g,
                    longWeaponPlace.GetComponent<Image>().color.b,
                    0f
                    );
                weaponsHandlerOfPlayerInMainMenu.GetComponent<weaponsHandlerSingleplayer>()?.SelectWeaponById(weapon.GetComponent<GunStats>().thisWeapon.weaponIndex);
                break;
            }
        }
        foreach (GameObject weapon in shortWeapons)
        {
            GunStats gunStats = weapon.GetComponent<GunStats>();
            Weapon weaponStats = gunStats.thisWeapon;
            Debug.Log("weapon index: " + weaponStats.weaponIndex);
            if (weaponStats.weaponIndex == loadout.shortSlotWeapon)
            {
                Debug.Log("short weapon found");
                LogWriter.WriteLog("short weapon found: " + weaponStats.weaponIndex);
                weapon.transform.SetParent(shortWeaponPlace.transform);
                weapon.transform.position = shortWeaponPlace.transform.position;
                weapon.transform.rotation = shortWeaponPlace.transform.rotation;
                shortWeaponPlace.GetComponent<Image>().color = new Color(
                    shortWeaponPlace.GetComponent<Image>().color.r,
                    shortWeaponPlace.GetComponent<Image>().color.g,
                    shortWeaponPlace.GetComponent<Image>().color.b,
                    0f
                );
                break;
            }
        }
        foreach (GameObject weapon in meleeWeapons)
        {
            GunStats gunStats = weapon.GetComponent<GunStats>();
            Weapon weaponStats = gunStats.thisWeapon;
            Debug.Log("weapon index: " + weaponStats.weaponIndex);
            if (weaponStats.weaponIndex == loadout.meleeSlotWeapon)
            {
                Debug.Log("melee weapon found");
                LogWriter.WriteLog("melee weapon found: " + weaponStats.weaponIndex);
                weapon.transform.SetParent(meleeWeaponPlace.transform);
                weapon.transform.position = meleeWeaponPlace.transform.position;
                weapon.transform.rotation = meleeWeaponPlace.transform.rotation;
                meleeWeaponPlace.GetComponent<Image>().color = new Color(
                    meleeWeaponPlace.GetComponent<Image>().color.r,
                    meleeWeaponPlace.GetComponent<Image>().color.g,
                    meleeWeaponPlace.GetComponent<Image>().color.b,
                    0f
                );
                break;
            }
        }
    }

    public void RandomizeWeaponLoadout()
    {
        return;
        GameObject randomLongWeapon = longWeapons[Random.Range(0, longWeapons.Length - 1)];
        randomLongWeapon.transform.SetParent(longWeaponPlace.transform);
        randomLongWeapon.transform.position = longWeaponPlace.transform.position;
        randomLongWeapon.transform.rotation = longWeaponPlace.transform.rotation;
        longWeaponPlace.GetComponent<Image>().color = new Color(
            longWeaponPlace.GetComponent<Image>().color.r,
            longWeaponPlace.GetComponent<Image>().color.g,
            longWeaponPlace.GetComponent<Image>().color.b,
            0f
            );
        GameObject randomShortWeapon = shortWeapons[Random.Range(0, shortWeapons.Length - 1)];
                randomShortWeapon.transform.SetParent(shortWeaponPlace.transform);
                randomShortWeapon.transform.position = shortWeaponPlace.transform.position;
                randomShortWeapon.transform.rotation = shortWeaponPlace.transform.rotation;
                shortWeaponPlace.GetComponent<Image>().color = new Color(
                    shortWeaponPlace.GetComponent<Image>().color.r,
                    shortWeaponPlace.GetComponent<Image>().color.g,
                    shortWeaponPlace.GetComponent<Image>().color.b,
                    0f
                );
            
        GameObject randomMeleeWeapon = meleeWeapons[Random.Range(0, meleeWeapons.Length - 1)];
                randomMeleeWeapon.transform.SetParent(meleeWeaponPlace.transform);
                randomMeleeWeapon.transform.position = meleeWeaponPlace.transform.position;
                randomMeleeWeapon.transform.rotation = meleeWeaponPlace.transform.rotation;
                meleeWeaponPlace.GetComponent<Image>().color = new Color(
                    meleeWeaponPlace.GetComponent<Image>().color.r,
                    meleeWeaponPlace.GetComponent<Image>().color.g,
                    meleeWeaponPlace.GetComponent<Image>().color.b,
                    0f
                );
        SaveWeaponLoadout();
    }

    public void SaveWeaponLoadout()
    {
        LogWriter.WriteLog("Saving weapons loadout");
        weaponsLoadout loadout = new weaponsLoadout();
        if (longWeaponPlace.transform.childCount > 0)
        {
            GameObject longWeapon = longWeaponPlace.transform.GetChild(0).gameObject;
            GunStats gunStats = longWeapon.GetComponent<GunStats>();
            Weapon weaponStats = gunStats.thisWeapon;
            loadout.longSlotWeapon = weaponStats.weaponIndex;
            weaponsHandlerOfPlayerInMainMenu.GetComponent<weaponsHandlerSingleplayer>()?.SelectWeaponById(longWeapon.GetComponent<GunStats>().thisWeapon.weaponIndex);
        }
        if (shortWeaponPlace.transform.childCount > 0)
        {
            GameObject shortWeapon = shortWeaponPlace.transform.GetChild(0).gameObject;
            GunStats gunStats = shortWeapon.GetComponent<GunStats>();
            Weapon weaponStats = gunStats.thisWeapon;
            loadout.shortSlotWeapon = weaponStats.weaponIndex;
        }
        if (meleeWeaponPlace.transform.childCount > 0)
        {
            GameObject meleeWeapon = meleeWeaponPlace.transform.GetChild(0).gameObject;
            GunStats gunStats = meleeWeapon.GetComponent<GunStats>();
            Weapon weaponStats = gunStats.thisWeapon;
            loadout.meleeSlotWeapon = weaponStats.weaponIndex;
        }
        dataTransfer.GetComponent<dataTransfer>().selectedWeapons = new List<int>();
        dataTransfer.GetComponent<dataTransfer>().selectedWeapons.Add(loadout.longSlotWeapon);
        dataTransfer.GetComponent<dataTransfer>().selectedWeapons.Add(loadout.shortSlotWeapon);
        dataTransfer.GetComponent<dataTransfer>().selectedWeapons.Add(loadout.meleeSlotWeapon);
        string json = JsonUtility.ToJson(loadout);
        File.WriteAllText(Application.persistentDataPath + "/weaponsLoadout.json", json);
    }
}

public class weaponsLoadout
{
    public int longSlotWeapon;
    public int shortSlotWeapon;
    public int meleeSlotWeapon;
}