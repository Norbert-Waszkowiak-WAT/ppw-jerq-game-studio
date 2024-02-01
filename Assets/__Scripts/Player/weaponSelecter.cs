using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponSelecter : MonoBehaviour
{
    public List<GameObject> weapons;

    public GameObject weaponsHolder;
    public weaponsHandler weaponsHandlerGot;

    public void ChooseWeapons(List<int> weaponIndexes)
    {
        LogWriter.WriteLog("weaponSelecter.ChooseWeapons(" + weaponIndexes + ") called");
        weaponsHandlerGot.weapons.Clear();
        foreach(int weaponIndex in weaponIndexes)
        {
            GameObject foundWeapon = null;
            foreach(GameObject weapon in weapons)
            {
                GunStats gunStatsGot = weapon.GetComponent<GunStats>();
                if (gunStatsGot.thisWeapon.weaponIndex == weaponIndex)
                {
                    foundWeapon = weapon;
                }
            }
            if (foundWeapon != null)
            {
                foundWeapon.transform.SetParent(weaponsHolder.transform);
                weaponsHandlerGot.weapons.Add(foundWeapon);
            }
        }
        weaponsHandlerGot.SelectWeapon(0);
    }
}
